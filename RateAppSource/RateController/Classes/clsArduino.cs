﻿using System;
using System.Diagnostics;

namespace RateController
{
    public class clsArduino
    {
        private float cIntegral = 0;
        private readonly clsProduct Prd;

        private bool ApplicationOn = false;
        private bool AutoOn = true;
        private bool CalOn;
        private byte CalPWM;
        private ControlTypeEnum ControlType;        

        private double CurrentDuration;
        private bool cUseMultiPulse;
        private double cScaleCounts;
        private int cErrorRange = 4;        // % random error in flow rate, above and below target


        private DateTime LastPulse = DateTime.Now;
        private float LastPWM;
        private DateTime LastTime;
        private DateTime LastScaleCountTime;
        private int LOOP_TIME = 50;
        private float ManualAdjust;
        private DateTime ManualLast;
        private bool MasterOn = false;
        private byte MaxRPM = 255;
        private float MaxSimUPM = 40F;        // max rate of system in UPM
        private int mcID;
        private float MeterCal;

        private float Oave;

        private UInt16 Ocount;

        private UInt32 Omax;

        private UInt32 Omax2;

        private UInt32 Omin;

        private UInt32 Omin2;

        private UInt32 Osum;

        // PID
        private byte PIDbrakePoint = 20;

        private byte PIDdeadband = 3;
        private byte PIDHighMax = 255;
        private byte PIDki = 0;
        private byte PIDkp = 20;
        private byte PIDLowMax = 100;
        private byte PIDminPWM = 50;

        private byte PowerRelayHi;
        private byte PowerRelayLo;
        private UInt32 PPM;
        private float PPR = 1.0F;        // pulses per revolution

        private double PrevCount;
        private double pulseCount;
        private float pulseDuration;
        private float Pulses = 0;

        private float pwmSetting;
        private float rateError;
        private float rateSetPoint;
        private DateTime ReceiveTime;
        private byte RelayHi;
        private byte RelayLo;
        private float SimRPM = 0.0F;

        private int SimulateInterval;
        private DateTime SimulateTimeLast;

        private double TimedCounts;
        private DateTime TimedLast = DateTime.Now;
        private float TotalPulses;

        private float UPM;

        private float ValveAdjust = 0;   // % amount to open/close valve
        private float ValveOpen = 0;      // % valve is open
        private float ValveOpenTime = 4000;  // ms to fully open valve at max opening rate

        public float Integral { get => cIntegral; set => cIntegral = value; }
        public int ErrorRange { get => cErrorRange; set => cErrorRange = value; }

        public clsArduino(clsProduct CalledFrom)
        {
            Prd = CalledFrom;
            cScaleCounts = 50000;
            LastScaleCountTime = DateTime.Now;
        }

        public void MainLoop()
        {
            // ReceiveSerial();
            byte RelayControl;

            RelayControl = RelayLo;
            // RelayToAOG = 0;

            bool ControllerConnected = ((DateTime.Now - ReceiveTime).TotalSeconds < 4);

            ApplicationOn = (ControllerConnected && (RelayControl != 0) && (rateSetPoint > 0)) || (ControlType == ControlTypeEnum.Fan);

            if ((DateTime.Now - LastTime).TotalMilliseconds >= LOOP_TIME)
            {
                LastTime = DateTime.Now;
                if (AutoOn)
                {
                    switch (ControlType)
                    {
                        case ControlTypeEnum.Motor:
                        case ControlTypeEnum.MotorWeights:
                        case ControlTypeEnum.Fan:
                            // motor control
                            SimulateMotor(PIDHighMax);
                            rateError = rateSetPoint - GetUPM();

                            if (CalOn)
                            {
                                pwmSetting = CalPWM;
                            }
                            else
                            {
                                pwmSetting = ControlMotor(PIDkp, rateError, rateSetPoint, PIDminPWM,
                                    PIDHighMax, PIDdeadband);
                            }
                            break;

                        default:
                            // valve control

                            if (CalOn)
                            {
                                pwmSetting = CalPWM;
                            }
                            else
                            {
                                SimulateValve(PIDminPWM, PIDHighMax);
                                rateError = rateSetPoint - GetUPM();
                                pwmSetting = DoPID(PIDkp, rateError, rateSetPoint, PIDminPWM, PIDLowMax,
                                    PIDHighMax, PIDbrakePoint, PIDdeadband, PIDki);
                            }
                            break;
                    }
                }
                else
                {
                    // manual control
                    if (CalOn)
                    {
                        pwmSetting = CalPWM;
                    }
                    else
                    {
                        if ((DateTime.Now - ManualLast).TotalSeconds > 1.5)
                        {
                            ManualLast = DateTime.Now;

                            // adjust rate
                            if (rateSetPoint == 0) rateSetPoint = 1;

                            switch (ControlType)
                            {
                                case ControlTypeEnum.Motor:
                                case ControlTypeEnum.MotorWeights:
                                case ControlTypeEnum.Fan:
                                    // motor control
                                    if (ManualAdjust > 0)
                                    {
                                        pwmSetting *= (float)1.10;
                                        if (pwmSetting < 1) pwmSetting = PIDminPWM;
                                    }
                                    else if (ManualAdjust < 0)
                                    {
                                        pwmSetting *= (float)0.90;
                                        if (pwmSetting < PIDminPWM) pwmSetting = 0;
                                    }
                                    break;

                                default:
                                    // valve control
                                    pwmSetting = ManualAdjust;
                                    break;
                            }
                        }
                    }

                    switch (ControlType)
                    {
                        // calculate application rate
                        case ControlTypeEnum.Motor:
                        case ControlTypeEnum.MotorWeights:
                        case ControlTypeEnum.Fan:
                            // motor control
                            SimulateMotor(PIDHighMax);
                            rateError = rateSetPoint - GetUPM();
                            break;

                        default:
                            // valve control
                            SimulateValve(PIDminPWM, PIDHighMax);
                            rateError = rateSetPoint - GetUPM();
                            break;
                    }
                }
                SendSerial();

                if (ApplicationOn) SimulateWeightPGN();
            }
        }

        public void ReceiveSerial(byte[] Data)
        {
            //PGN32614 to Arduino from Rate Controller, 16 bytes
            //0	HeaderLo		102
            //1	HeaderHi		127
            //2 Controller ID
            //3	relay Lo		0 - 7
            //4	relay Hi		8 - 15
            //5	rate set Lo		10 X actual
            //6 rate set Mid
            //7	rate set Hi		10 X actual
            //8	Flow Cal Lo		1000 X actual
            //9	Flow Cal Mid
            //10 Flow Cal Hi
            //11	Command
            //	        - bit 0		    reset acc.Quantity
            //	        - bit 1,2,3		control type 0-4
            //	        - bit 4		    MasterOn
            //          - bit 5         0 - average time for multiple pulses, 1 - time for one pulse
            //          - bit 6         AutoOn
            //          - bit 7         Calibration On
            //12    power relay Lo      list of power type relays 0-7
            //13    power relay Hi      list of power type relays 8-15
            //14    Cal PWM     calibration pwm
            //15    crc

            byte InCommand;
            int Tmp;
            double RateIn = 0;

            int PGN = Data[1] << 8 | Data[0];
            if (PGN == 32614)
            {
                mcID = Data[2];

                RelayLo = Data[3];
                RelayHi = Data[4];

                // rate setting, 10 times actual
                Tmp = Data[5] | Data[6] << 8 | Data[7] << 16;
                RateIn = (double)Tmp * 0.1;

                // meter cal, 1000 times actual
                Tmp = Data[8] | Data[9] << 8 | Data[10] << 16;
                MeterCal = (float)(Tmp * 0.001);

                // command byte
                InCommand = Data[11];
                if ((InCommand & 1) == 1) TotalPulses = 0;    // reset accumulated count

                ControlType = 0;
                if ((InCommand & 2) == 2) ControlType += 1;
                if ((InCommand & 4) == 4) ControlType += 2;
                if ((InCommand & 8) == 8) ControlType += 4;

                MasterOn = ((InCommand & 16) == 16);
                cUseMultiPulse = ((InCommand & 32) == 32);

                AutoOn = ((InCommand & 64) == 64);
                if (AutoOn)
                {
                    rateSetPoint = (float)(RateIn);
                }
                else
                {
                    ManualAdjust = (float)(RateIn);
                }

                CalOn = ((InCommand & 128) == 128);

                PowerRelayLo = Data[12];
                PowerRelayHi = Data[13];
                CalPWM = Data[14];

                ReceiveTime = DateTime.Now;
            }

            if (PGN == 32616)
            {
                byte ConID = Data[2];
                PIDkp = Data[3];
                PIDminPWM = Data[4];
                PIDLowMax = Data[5];
                PIDHighMax = Data[6];
                PIDdeadband = Data[7];
                PIDbrakePoint = Data[8];
                PIDki = Data[10];

                ReceiveTime = DateTime.Now;
            }

            if (RateIn > MaxSimUPM)
            {
                MaxSimUPM = (float)(RateIn * 1.5);
            }
        }

        private int ControlMotor(byte sKP, float sError, float sSetPoint, byte sMinPWM,
             byte sHighMax, byte sDeadband)
        {
            float Result = 0;
            float ErrorPercent = 0;
            if (sSetPoint > 0 && ApplicationOn)
            {
                Result = LastPWM;
                ErrorPercent = (float)(Math.Abs(sError / sSetPoint) * 100.0);
                float Max = (float)sHighMax;

                if (ErrorPercent > (float)sDeadband)
                {
                    Result += (float)((float)sKP / 255.0) * sError;

                    if (Result > Max) Result = Max;
                    if (Result < sMinPWM) Result = (float)sMinPWM;
                }
            }

            LastPWM = Result;
            return (int)Result;
        }

        private int DoPID(byte clKP, float clError, float clSetPoint, byte clMinPWM, byte clLowMax, byte clHighMax, byte clBrakePoint, byte clDeadband, byte clKi)
        {
            try
            {
                int Result = 0;
                if (ApplicationOn)
                {
                    float ErrorPercent = Math.Abs(clError / clSetPoint);
                    float ErrorBrake = (float)(clBrakePoint / 100.0);
                    float Max = (float)clHighMax;

                    if (ErrorPercent > ((float)(clDeadband / 100.0)))
                    {
                        if (ErrorPercent <= ErrorBrake) Max = clLowMax;

                        Result = (int)((clKP * clError) + (Integral * clKi / 255.0));

                        bool IsPositive = (Result > 0);
                        Result = Math.Abs(Result);

                        if (Result != 0)
                        {
                            // limit integral size
                            if ((Integral / Result) < 4) Integral += clError / (float)3.0;
                            //else Integral -= clError;
                        }

                        if (Result > Max) Result = (int)Max;
                        else if (Result < clMinPWM) Result = clMinPWM;

                        if (!IsPositive) Result *= -1;
                    }
                    else
                    {
                        Integral = 0;
                    }
                }
                return Result;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private float GetUPM()
        {
            if (ControlType == ControlTypeEnum.MotorWeights)
            {
                UPM = MeterCal * pwmSetting;
                return UPM;
            }
            else
            {
                return GetUPMflow();
            }
        }

        private float GetUPMflow()
        {
            double CurrentCounts;
            int RateInterval;

            if (pulseCount > 0)
            {
                CurrentCounts = pulseCount;
                pulseCount = 0;
                CurrentDuration = pulseDuration;

                if (cUseMultiPulse)
                {
                    // low ms/pulse, use pulses over time
                    TimedCounts += CurrentCounts;
                    RateInterval = (int)(DateTime.Now - TimedLast).TotalMilliseconds;
                    if (RateInterval > 500)
                    {
                        TimedLast = DateTime.Now;
                        PPM = (uint)((6000000 * TimedCounts) / RateInterval);  // 100 X actual
                        TimedCounts = 0;
                    }
                }
                else
                {
                    // high ms/pulse, use time for one pulse
                    if (CurrentDuration > 0)
                    {
                        PPM = (uint)(6000000 / CurrentDuration); // 100 X actual
                    }
                    else
                    {
                        PPM = 0;
                    }
                }

                LastPulse = DateTime.Now;
                TotalPulses += (float)CurrentCounts;
            }

            if ((DateTime.Now - LastPulse).TotalMilliseconds > 4000) PPM = 0;   // check for no flow
            // olympic average
            if (Omax < PPM)
            {
                Omax2 = Omax;
                Omax = PPM;
            }
            else if (Omax2 < PPM) Omax2 = PPM;

            if (Omin > PPM)
            {
                Omin2 = Omin;
                Omin = PPM;
            }
            else if (Omin2 > PPM) Omin2 = PPM;

            Osum += PPM;
            Ocount++;
            if (Ocount > 9)
            {
                Osum -= Omax;
                Osum -= Omin;
                Osum -= Omax2;
                Osum -= Omin2;
                Oave = (float)((float)Osum / 600.0);  // divide by 3 and divide by 100
                Osum = 0;
                Omax = 0;
                Omin = 5000000;
                Omax2 = 0;
                Omin2 = 5000000;
                Ocount = 0;
            }

            // units per minute
            if (MeterCal > 0)
            {
                UPM = Oave / MeterCal;
            }
            else
            {
                UPM = 0;
            }
            return UPM;
        }

        private void SendSerial()
        {
            // PGN 32613
            byte Temp;

            string[] words = new string[13];
            words[0] = "101";
            words[1] = "127";
            words[2] = mcID.ToString();

            // rate applied, 10 X actual
            Temp = (byte)(UPM * 10);
            words[3] = Temp.ToString();
            Temp = (byte)((int)(UPM * 10) >> 8);
            words[4] = Temp.ToString();
            Temp = (byte)((int)(UPM * 10) >> 16);
            words[5] = Temp.ToString();

            // accumulated quantity
            int Units = (int)(TotalPulses * 10 / MeterCal);
            Temp = (byte)Units;
            words[6] = Temp.ToString();
            Temp = (byte)(Units >> 8);
            words[7] = Temp.ToString();
            Temp = (byte)(Units >> 16);
            words[8] = Temp.ToString();

            //pwmSetting
            Temp = (byte)((int)(pwmSetting));
            words[9] = Temp.ToString();
            Temp = (byte)((int)(pwmSetting) >> 8);
            words[10] = Temp.ToString();

            // status
            // bit 0    - sensor 0 receiving rate controller data
            // bit 1    - sensor 1 receiving rate controller data
            words[11] = "3";

            // crc
            words[12] = Prd.mf.Tls.CRC(words, 13).ToString();

            Prd.SerialFromAruduino(words, false);
        }

        private void SimulateMotor(byte sMax)
        {
            float SimTmp;

            if (ApplicationOn)
            {
                if (pwmSetting > 250) pwmSetting = 255;
                if (pwmSetting < -250) pwmSetting = -255;

                SimulateInterval = (int)(DateTime.Now - SimulateTimeLast).TotalMilliseconds;
                SimulateTimeLast = DateTime.Now;

                SimRPM += (float)(((pwmSetting / (float)sMax) * MaxRPM - SimRPM) * 0.35);    // update rpm
                Prd.mf.Tls.NoisyData(SimRPM, ErrorRange);    // add error
                if (SimRPM < 0) SimRPM = 0;

                SimTmp = PPR * SimRPM;
                if (SimTmp > 0)
                {
                    pulseDuration = ((float)(60000.0 / SimTmp));
                }
                else
                {
                    pulseDuration = 0;
                }

                double CurCount = SimRPM * PPR;
                PrevCount += CurCount * (SimulateInterval / 60000.0); // counts for time slice
                if (PrevCount > 1)
                {
                    pulseCount = PrevCount;
                    PrevCount = 0;
                }
            }
            else
            {
                pulseCount = 0;
            }
        }

        private void SimulateValve(byte Min, byte Max)
        {
            float PulseTime;
            float SimUPM;

            SimulateInterval = (int)(DateTime.Now - SimulateTimeLast).TotalMilliseconds;
            SimulateTimeLast = DateTime.Now;

            if (ApplicationOn)
            {
                float Range = Max - Min + 5;
                if (Range == 0 || pwmSetting == 0)
                {
                    ValveAdjust = 0;
                }
                else
                {
                    float Percent = (float)((Math.Abs(pwmSetting) - Min + 5) / Range);
                    if (pwmSetting < 0)
                    {
                        Percent *= -1;
                    }

                    ValveAdjust = (float)(Percent * (float)(SimulateInterval / ValveOpenTime) * 100.0);
                }

                ValveOpen += ValveAdjust;
                if (ValveOpen < 0) ValveOpen = 0;
                if (ValveOpen > 100) ValveOpen = 100;
            }
            else
            {
                ValveOpen = 0;
            }

            SimUPM = (float)(MaxSimUPM * ValveOpen / 100.0);

            Pulses = (float)((SimUPM * MeterCal) / 60000.0);  // (Units/min * pulses/Unit) = pulses/min / 60000 = pulses/millisecond
            if (Pulses == 0)
            {
                pulseCount = 0;
                pulseDuration = 0;
            }
            else
            {
                PulseTime = (float)(1.0 / Pulses);   // milliseconds for each pulse

                PulseTime = (float)Prd.mf.Tls.NoisyData(PulseTime, ErrorRange);
                PrevCount += SimulateInterval / PulseTime; // milliseconds * pulses/millsecond = pulses
                if (PrevCount > 1)
                {
                    pulseCount = PrevCount;
                    PrevCount = 0;
                }

                // pulse duration is the time for one pulse
                pulseDuration = PulseTime;
            }
        }

        private void SimulateWeightPGN()
        {
            // PGN 32501
            double ScaleCountsChange;

            byte[] Data = new byte[8];
            Data[0] = 245;
            Data[1] = 126;
            Data[2] = (byte)mcID;

            ScaleCountsChange = (DateTime.Now - LastScaleCountTime).TotalMinutes * rateSetPoint * Prd.ScaleCountsPerUnit;
            LastScaleCountTime = DateTime.Now;
            ScaleCountsChange = Prd.mf.Tls.NoisyData(ScaleCountsChange, ErrorRange);
            cScaleCounts -= ScaleCountsChange;
            if (cScaleCounts < 0) cScaleCounts = 50000;

            int tmp = (int)cScaleCounts;
            Data[3]=(byte)tmp; 
            Data[4] = ((byte)(tmp >> 8));
            Data[5] = ((byte)(tmp >> 16));
            Data[6] = ((byte)(tmp >> 24));

            // crc
            Data[7] = Prd.mf.Tls.CRC(Data, 7);

            Prd.Scale.ParseByteData(Data);
        }
    }
}