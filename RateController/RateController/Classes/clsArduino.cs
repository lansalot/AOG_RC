﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RateController
{
    public class clsArduino
    {
        private string Sentence;
        byte Temp;

        int TotalPulses;
        float MeterCal;
        float pwmSetting;

        private readonly CRateCals RC;
        byte RelayHi;
        byte RelayFromAOG;

        float rateSetPoint;
        byte InCommand;

        byte MinPWMvalue;
        byte MaxPWMvalue = 255;

        bool ControllerConnected;
        int Tmp;
        byte ControlType;   // 0 standard, 1 fast close, 2 motor

        bool SimulateFlow;
        byte RelayControl;
        bool ApplicationOn = false;

        float rateError;

        bool AutoOn;
        bool RateUpMan = false;
        bool RateDownMan = false;
        int pwmManualRatio = 0;

        int pulseCount;
        int pulseDuration;
        float UPM;

        DateTime LastTime;
        int LOOP_TIME = 200;

        float ValveAdjust = 0;   // % amount to open/close valve
        float ValveOpen = 0;      // % valve is open
        float Pulses = 0;
        float ValveOpenTime = 4000;  // ms to fully open valve at max opening rate
        float SimUPM = 0;     // simulated units per minute
        float MaxRate = 120;  // max rate of system in UPM
        int ErrorRange = 4;  // % random error in flow rate, above and below target
        float PulseTime = 0;

        DateTime SimulateTimeLast;
        int SimulateInterval;
        float RandomError;

        long OldVCN;
        byte VCNbacklash;
        byte VCNspeed;
        byte VCNbrake;
        byte VCNdeadband;

        int NewPWM;
        float VCNerror;
        int CurrentCounts;

        DateTime SendStart;
        DateTime WaitStart;

        byte AdjustmentState = 0;	// 0 waiting, 1 sending pwm

        long SendTime = 300;    // ms pwm is sent to valve
        long WaitTime = 750;    // ms to wait before adjusting valve again
        byte SlowSpeed = 9;     // low pwm rate
        long VCN = 743;

        bool LastDirectionPositive;     // adjustment direction
        bool UseBacklashAdjustment;
        int PartsTemp;

        float KalResult = 0.0F;
        float KalPc = 0.0F;
        float KalG = 0.0F;
        float KalP = 1.0F;
        float KalVariance = 0.01F;   // larger is more filtering
        float KalProcess = 0.005F;	// smaller is more filtering

        int mcID;
        bool UseVCN = true;

        // PID
        byte PIDkp = 20;
        byte PIDminPWM = 50;
        byte PIDLowMax = 100;
        byte PIDHighMax = 255;
        byte PIDdeadband = 3;
        byte PIDbrakePoint = 20;

        private DateTime ReceiveTime;

        public clsArduino(CRateCals CalledFrom)
        {
            RC = CalledFrom;
        }

        public void MainLoop()
        {
            // ReceiveSerial();

            RelayControl = RelayFromAOG;
            // RelayToAOG = 0;
            AutoOn = true;

            ControllerConnected = ((DateTime.Now - ReceiveTime).TotalSeconds < 4);

            ApplicationOn = (ControllerConnected & (RelayControl != 0) & (rateSetPoint > 0));

            if ((DateTime.Now - LastTime).TotalMilliseconds >= LOOP_TIME)
            {
                LastTime = DateTime.Now;

                switch (ControlType)
                {
                    case 2:
                        // motor control
                        if (SimulateFlow) SimulateMotor(PIDminPWM, PIDHighMax);
                        rateError = rateSetPoint - GetUPM();

                        if (AutoOn)
                        {
                            pwmSetting = ControlMotor(PIDkp, rateError, rateSetPoint, PIDminPWM,
                                PIDHighMax, PIDdeadband);
                        }
                        break;

                    default:
                        // valve control
                        if (SimulateFlow) SimulateValve(MaxPWMvalue, MinPWMvalue);
                        rateError = rateSetPoint - GetUPM();

                        if (AutoOn)
                        {
                            if (UseVCN)
                            {
                                pwmSetting = VCNpwm(rateError, rateSetPoint, MinPWMvalue, MaxPWMvalue,
                                    VCN, UPM, SendTime, WaitTime, SlowSpeed, ControlType);
                            }
                            else
                            {
                                pwmSetting = DoPID(PIDkp, rateError, rateSetPoint, PIDminPWM, PIDLowMax,
                                    PIDHighMax, PIDbrakePoint, PIDdeadband);
                            }
                        }
                        break;
                }
                SendSerial();
            }
        }

        private void SendSerial()
        {
            // PGN 32613

            Sentence = "127,101,";

            Sentence += mcID.ToString() + ",";

            // rate applied
            Temp = (byte)((int)(UPM * 100) >> 8);
            Sentence += Temp.ToString();
            Sentence += ",";
            Temp = (byte)(UPM * 100);
            Sentence += Temp.ToString();
            Sentence += ",";

            // accumulated quantity
            int Units = (int)(TotalPulses * 100 / MeterCal);
            Temp = (byte)(Units >> 16);
            Sentence += Temp.ToString();
            Sentence += ",";
            Temp = (byte)(Units >> 8);
            Sentence += Temp.ToString();
            Sentence += ",";
            Temp = (byte)Units;
            Sentence += Temp.ToString();
            Sentence += ",";

            //pwmSetting
            UInt16 Number = (UInt16)(pwmSetting * 10);
            byte[] Tmp = new byte[] { (byte)Number, (byte)(Number >> 8) };

            Sentence += Tmp[1].ToString();  // high byte
            Sentence += ",";
            Sentence += Tmp[0].ToString();  // low byte

            Sentence += "\r";
            RC.CommFromArduino(Sentence, false);
        }

        public void ReceiveSerial(byte[] Data)
        {
            int PGN = Data[0] << 8 | Data[1];
            if (PGN == 32614)
            {
                mcID = Data[2];

                RelayHi = Data[3];
                RelayFromAOG = Data[4];

                // rate setting, 100 times actual
                Tmp = Data[5] << 8 | Data[6];
                rateSetPoint = (float)(Tmp * .01);

                // meter cal, 100 times actual
                Tmp = Data[7] << 8 | Data[8];
                MeterCal = (float)(Tmp * .01);

                // command byte
                InCommand = Data[9];
                if ((InCommand & 1) == 1) TotalPulses = 0;    // reset accumulated count

                ControlType = 0;
                if ((InCommand & 2) == 2) ControlType += 1;
                if ((InCommand & 4) == 4) ControlType += 2;

                SimulateFlow = ((InCommand & 8) == 8);

                UseVCN = ((InCommand & 16) == 16);

                ReceiveTime = DateTime.Now;
            }

            if (PGN == 32615)
            {
                mcID = Data[2];
                VCN = (int)(Data[3] << 8 | Data[4]);
                SendTime = (int)(Data[5] << 8 | Data[6]);
                WaitTime = (int)(Data[7] << 8 | Data[8]);
                MinPWMvalue = Data[9];

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

                ReceiveTime = DateTime.Now;
            }

            if (PGN == 32617)
            {
                byte[] SecID = new byte[16];
                byte IDs = 0;
                byte Section = 0;

                for (int i = 0; i < 8; i++)
                {
                    IDs = Data[i + 2];
                    for (int j = 0; j < 4; j++)
                    {
                        if (RC.mf.Tls.IsBitSet(IDs, j))
                        {
                            SecID[Section] = (byte)j;
                            break;
                        }
                    }
                    Section++;

                    for (int j = 4; j < 8; j++)
                    {
                        if (RC.mf.Tls.IsBitSet(IDs, j))
                        {
                            SecID[Section] = (byte)(j - 4);
                            break;
                        }
                    }
                    Section++;
                }
            }
        }

        void SimulateValve(byte Max, byte Min)
        {
            var Rand = new Random();

            SimulateInterval = (int)(DateTime.Now - SimulateTimeLast).TotalMilliseconds;
            SimulateTimeLast = DateTime.Now;
            if (UseVCN)
            {
                Max = PIDHighMax;
                Min = PIDminPWM;
            }

            if (ApplicationOn)
            {
                if (AutoOn)
                {
                    float Range = Max - Min + 5;
                    if (Range == 0 | pwmSetting == 0)
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
                }
                else
                {
                    // manual control
                    ValveAdjust = 0;
                    if (RateUpMan) ValveAdjust = (float)((SimulateInterval / ValveOpenTime) * 100.0 * (pwmManualRatio / 100));
                    if (RateDownMan) ValveAdjust = (float)((SimulateInterval / ValveOpenTime) * -100.0 * (pwmManualRatio / 100));
                }

                ValveOpen += ValveAdjust;
                if (ValveOpen < 0) ValveOpen = 0;
                if (ValveOpen > 100) ValveOpen = 100;
            }
            else
            {
                ValveOpen = 0;
            }

            SimUPM = (float)(MaxRate * ValveOpen / 100.0);

            Pulses = (float)((SimUPM * MeterCal) / 60000.0);  // (Units/min * pulses/Unit) = pulses/min / 60000 = pulses/millisecond
            if (Pulses == 0)
            {
                pulseCount = 0;
                pulseDuration = 0;
            }
            else
            {
                PulseTime = (float)(1.0 / Pulses);   // milliseconds for each pulse

                RandomError = (100 - ErrorRange) + (Rand.Next(ErrorRange * 2));

                PulseTime = (float)(PulseTime * RandomError / 100);
                pulseCount = (int)(SimulateInterval / PulseTime); // milliseconds * pulses/millsecond = pulses

                // pulse duration is the time for one pulse
                pulseDuration = (int)PulseTime;
            }

        }

        float MaxRPM = 100.0F;
        float PPR = 50.0F;  // pulses per revolution
        float SimRPM = 0.0F;
        float SimTmp;

        void SimulateMotor(byte sMin, byte sMax)
        {
            if (ApplicationOn)
            {
                var Rand = new Random();

                SimulateInterval = (int)(DateTime.Now - SimulateTimeLast).TotalMilliseconds;
                SimulateTimeLast = DateTime.Now;

                SimRPM += (float)(((pwmSetting / (float)sMax) * MaxRPM - SimRPM) * 0.25);    // update rpm
                RandomError = (float)((100.0 - ErrorRange) + (Rand.Next((int)(ErrorRange * 2.0))));
                SimRPM = (float)(SimRPM * RandomError / 100.0);
                if (SimRPM < sMin) SimRPM = (float)sMin;


                SimTmp = PPR * SimRPM;
                if(SimTmp>0)
                {
                    pulseDuration = (int)(60000.0 / SimTmp);
                }
                else
                {
                    pulseDuration = 0;
                }

                pulseCount = (int)(SimRPM * PPR);
                pulseCount = (int)(pulseCount * (SimulateInterval / 60000.0)); // counts for time slice
            }
            else
            {
                pulseCount = 0;
            }
        }

        int VCNpwm(float cError, float cSetPoint, byte MinPWM, byte MaxPWM, long cVCN,
                    float cFlowRate, long cSendTime, long cWaitTime, byte cSlowSpeed, byte cValveType)
        {
            NewPWM = 0;
            if (ApplicationOn)
            {
                VCNparts(cVCN);

                // deadband
                float DB = (float)(VCNdeadband / 100.0) * cSetPoint;

                if (Math.Abs(cError) > DB)
                {
                    // backlash
                    if (!UseBacklashAdjustment && VCNbacklash > 0)
                    {
                        if ((cError >= 0 && !LastDirectionPositive) | (cError < 0 && LastDirectionPositive))
                        {
                            UseBacklashAdjustment = true;
                            SendStart = DateTime.Now;
                        }
                        LastDirectionPositive = (cError >= 0);
                    }

                    if (UseBacklashAdjustment)
                    {
                        if ((DateTime.Now - SendStart).TotalMilliseconds > (VCNbacklash * 10))
                        {
                            UseBacklashAdjustment = false;
                            LastDirectionPositive = (cError >= 0);
                            SendStart = DateTime.Now;
                        }
                        else
                        {
                            NewPWM = MaxPWM - (cSlowSpeed * (MaxPWM - MinPWM) / 9);
                            if (cError < 0) NewPWM *= -1;
                        }
                    }
                    else
                    {
                        if (AdjustmentState == 0)
                        {
                            // waiting
                            if ((DateTime.Now - WaitStart).TotalMilliseconds > cWaitTime)
                            {
                                // waiting finished
                                AdjustmentState = 1;
                                SendStart = DateTime.Now;
                            }
                        }

                        if (AdjustmentState == 1)
                        {
                            // sending pwm
                            if ((DateTime.Now - SendStart).TotalMilliseconds > cSendTime)
                            {
                                // sending finished
                                AdjustmentState = 0;
                                WaitStart = DateTime.Now;
                                NewPWM = 0;
                            }
                            else
                            {
                                // get new pwm value
                                if (cFlowRate == 0 && cValveType == 1)
                                {
                                    // open 'fast close' valve
                                    NewPWM = MaxPWM;
                                }
                                else
                                {
                                    // % error
                                    if (cSetPoint > 0)
                                    {
                                        VCNerror = (float)((cError / cSetPoint) * 100.0);
                                    }
                                    else
                                    {
                                        VCNerror = 0;
                                    }

                                    // set pwm value
                                    if (Math.Abs(VCNerror) < VCNbrake)
                                    {
                                        // slow adjustment
                                        NewPWM = MaxPWM - ((MaxPWM - MinPWM) * cSlowSpeed / 9);
                                    }
                                    else
                                    {
                                        // normal adjustment
                                        NewPWM = MaxPWM - ((MaxPWM - MinPWM) * VCNspeed / 9);
                                    }

                                    if (cError < 0) NewPWM *= -1;
                                }
                            }
                        }
                    }
                }
            }
            return NewPWM;
        }

        void VCNparts(long NewVCN)
        {
            if ((NewVCN != OldVCN) && (NewVCN <= 9999) && (NewVCN >= 0))
            {
                VCNbacklash = (byte)(NewVCN / 1000);
                PartsTemp = (int)(NewVCN - VCNbacklash * 1000);

                VCNspeed = (byte)(PartsTemp / 100);
                PartsTemp = PartsTemp - VCNspeed * 100;

                VCNbrake = (byte)(PartsTemp / 10);

                VCNdeadband = (byte)(PartsTemp - VCNbrake * 10);

                VCNbrake *= 10;
                if (VCNbrake == 0) VCNbrake = 5;

                OldVCN = NewVCN;
            }
        }

        int DoPID(byte clKP, float clError, float clSetPoint, byte clMinPWM, byte clLowMax, byte clHighMax, byte clBrakePoint, byte clDeadband)
        {
            int Result = 0;
            if (ApplicationOn)
            {
                float ErrorPercent = Math.Abs(clError / clSetPoint);
                float ErrorBrake = (float)((float)(clBrakePoint / 100.0));
                float Max = (float)clHighMax;

                if (ErrorPercent > ((float)(clDeadband / 100.0)))
                {
                    if (ErrorPercent <= ErrorBrake)
                    {
                        Max = (ErrorPercent / ErrorBrake) * clLowMax;
                    }

                    Result = (int)(clKP * clError);

                    bool IsPositive = (Result > 0);
                    Result = Math.Abs(Result);
                    if (Result > Max) Result = (int)Max;
                    if (Result < clMinPWM) Result = clMinPWM;
                    if (!IsPositive) Result *= -1;
                }
            }
            return Result;
        }

        float LastPWM;
        int ControlMotor(byte sKP, float sError, float sSetPoint, byte sMinPWM,
             byte sHighMax, byte sDeadband)
        {
            float Result = 0;
            float ErrorPercent = 0;
            if (ApplicationOn)
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

        DateTime LastPulse = DateTime.Now;
        DateTime TimedLast = DateTime.Now;
        int CurrentDuration;
        float PPM;
        int MinPulseTime;
        int TimedCounts;
        int LowMsPulseTrigger = 50;
        int RateInterval;

        float GetUPM()
        {
            SetMinPulseTime();

            // check for no PPM
            if ((DateTime.Now - LastPulse).TotalMilliseconds > 4000)
            {
                pulseDuration = 0;
                CurrentDuration = 0;
                PPM = 0;
            }
            if (pulseCount > 0)
            {
                LastPulse = DateTime.Now;
            }


            // accumulated total
            CurrentCounts = pulseCount;
            pulseCount = 0;
            TotalPulses += CurrentCounts;

            // ppm
            if (MinPulseTime == 0)
            {
                // low ms/pulse
                TimedCounts += CurrentCounts;
                RateInterval = (int)(DateTime.Now - TimedLast).TotalMilliseconds;
                if (RateInterval > 200)
                {
                    TimedLast = DateTime.Now;
                    PPM = (float)((60000.0 * TimedCounts) / RateInterval);
                    TimedCounts = 0;
                }
            }
            else
            {
                // high ms/pulse
                if (pulseDuration > MinPulseTime) CurrentDuration = pulseDuration;
                if (CurrentDuration > 0) PPM = 60000 / CurrentDuration;
            }

            // Kalmen filter
            KalPc = KalP + KalProcess;
            KalG = KalPc / (KalPc + KalVariance);
            KalP = (1 - KalG) * KalPc;
            KalResult = KalG * (PPM - KalResult) + KalResult;
            PPM = KalResult;

            // units per minute
            if (MeterCal > 0)
            {
                UPM = PPM / MeterCal;
            }
            else
            {
                UPM = 0;
            }

            return UPM;
        }

        void SetMinPulseTime()
        {
            // ms/pulse = 60000 / ((units per minute) * (counts per unit))
            float Ms = rateSetPoint * MeterCal;
            if (Ms > 0)
            {
                Ms = (float)(60000.0 / Ms);
            }
            else
            {
                Ms = 0;
            }

            if (Ms < LowMsPulseTrigger)
            {
                // low ms/pulse
                MinPulseTime = 0;
            }
            else
            {
                // high ms/pulse
                MinPulseTime = 5;
            }
        }
    }
}
