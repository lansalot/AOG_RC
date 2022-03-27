﻿using AgOpenGPS;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace RateController
{
    public partial class frmPCBsettings : Form
    {
        public TextBox[] CFG;
        public CheckBox[] CKs;
        public FormStart mf;
        private bool Initializing = false;
        private bool[] TabEdited;

        public frmPCBsettings(FormStart CallingForm)
        {
            InitializeComponent();

            mf = CallingForm;

            CFG = new TextBox[] {tbIMUdelay,tbIMUinterval,tbZeroOffset,tbMinSpeed
            ,tbMaxSpeed,tbPulseCal,tbRTCM,tbAdsWasPin,tbModule,tbPowerRelay
            ,tbRS485port,tbDir1,tbPwm1,tbSteerSwitch,tbWAS,tbSteerRelay,tbWorkSwitch
            ,tbCurrentSensor,tbPressureSensor,tbEncoder,tbDir2,tbPwm2
            ,tbSpeedPulse,tbSendEnable};

            for (int i = 0; i < CFG.Length; i++)
            {
                CFG[i].Tag = i;
                CFG[i].Enter += tb_Enter;
                CFG[i].TextChanged += tb_TextChanged;
                CFG[i].Validating += tb_Validating;
            }

            CKs = new CheckBox[] {ckGyro,ckGGA,ckUseRate,ckADS,ckRelayOn,ckFlowOn
            ,ckSwapPitchRoll,ckInvertRoll};

            TabEdited = new bool[3];
        }

        private void bntOK_Click(object sender, EventArgs e)
        {
            try
            {
                Button ButtonClicked = (Button)sender;
                if (ButtonClicked.Text == Lang.lgClose)
                {
                    bool Edited = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (TabEdited[i])
                        {
                            Edited = true;
                            break;
                        }
                    }
                    if (Edited) mf.Tls.ShowHelp("Changes have not been sent to the module.", "Warning", 3000);

                    this.Close();
                }
                else
                {
                    SaveSettings();
                    SetButtons(false);
                    UpdateForm();

                    //// update module
                    //bool Updated = false;
                    //if (TabEdited[0])
                    //{
                    //    PGN32622 PGN = new PGN32622(this);
                    //    PGN.Send(!TabEdited[1] && !TabEdited[2]);
                    //    Updated = true;
                    //}

                    //if (TabEdited[1])
                    //{
                    //    PGN32623 PGN = new PGN32623(this);
                    //    PGN.Send(!TabEdited[2]);
                    //    Updated = true;
                    //}

                    //if (TabEdited[2])
                    //{
                    //    PGN32624 PGN = new PGN32624(this);
                    //    PGN.Send(true);
                    //    Updated = true;
                    //}

                    //if (Updated)
                    //{
                    //    mf.Tls.ShowHelp("Sent to module.", this.Text, 3000);
                    //}
                }
            }
            catch (Exception ex)
            {
                mf.Tls.ShowHelp(ex.Message, this.Text, 3000, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                TabEdited[i] = false;
            }
            UpdateForm();
            SetButtons(false);
        }

        private void btnLoadDefaults_Click(object sender, EventArgs e)
        {
            // AS13_PCB
            cbReceiver.SelectedIndex = 1;
            cbIMU.SelectedIndex = 1;
            tbIMUdelay.Text = "90";
            tbIMUinterval.Text = "40";
            tbZeroOffset.Text = "6100";
            tbMinSpeed.Text = "1";
            tbMaxSpeed.Text = "15";
            tbPulseCal.Text = "25.5";

            tbModule.Text = "0";
            tbPowerRelay.Text = "255";
            tbRTCM.Text = "5432";
            tbAdsWasPin.Text = "1";
            tbRS485port.Text = "7";
            ckGyro.Checked = false;
            ckGGA.Checked = true;
            ckUseRate.Checked = false;
            ckADS.Checked = false;
            ckRelayOn.Checked = false;
            ckFlowOn.Checked = false;
            ckSwapPitchRoll.Checked = false;
            ckInvertRoll.Checked = false;

            tbDir1.Text = "22";
            tbPwm1.Text = "23";
            tbDir2.Text = "36";
            tbPwm2.Text = "37";
            tbSteerRelay.Text = "2";
            tbWorkSwitch.Text = "32";
            tbSteerSwitch.Text = "39";
            tbEncoder.Text = "38";
            tbCurrentSensor.Text = "10";
            tbWAS.Text = "25";
            tbPressureSensor.Text = "26";
            tbSpeedPulse.Text = "11";
            tbSendEnable.Text = "27";
        }

        private void btnSendToModule_Click(object sender, EventArgs e)
        {
            try
            {
                PGN32622 PGN = new PGN32622(this);
                PGN.Send();
                PGN32623 PGN2 = new PGN32623(this);
                PGN2.Send();
                PGN32624 PGN3 = new PGN32624(this);
                PGN3.Send(true);

                mf.Tls.ShowHelp("Sent to module.", this.Text, 3000);

                for (int i = 0; i < 3; i++)
                {
                    TabEdited[i] = false;
                }
            }
            catch (Exception ex)
            {
                mf.Tls.ShowHelp(ex.Message, this.Text, 3000, true);
            }
        }

        private void frmPCBsettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                mf.Tls.SaveFormData(this);
            }
        }

        private void frmPCBsettings_Load(object sender, EventArgs e)
        {
            try
            {
                mf.Tls.LoadFormData(this);

                this.BackColor = Properties.Settings.Default.DayColour;

                for (int i = 0; i < tabControl1.TabCount; i++)
                {
                    tabControl1.TabPages[i].BackColor = Properties.Settings.Default.DayColour;
                }

                UpdateForm();
            }
            catch (Exception ex)
            {
                mf.Tls.ShowHelp(ex.Message, this.Text, 3000, true);
            }
        }

        private void LoadSettings()
        {
            try
            {
                byte tmp;
                bool Checked;
                double val;

                // textboxes
                for (int i = 0; i < CFG.Length; i++)
                {
                    double.TryParse(mf.Tls.LoadProperty(CFG[i].Name), out val);
                    if (i == 5)
                    {
                        // pulse cal
                        CFG[i].Text = val.ToString("N1");
                    }
                    else if (i == 6)
                    {
                        // RTCM port
                        CFG[i].Text = val.ToString("#######");
                    }
                    else
                    {
                        CFG[i].Text = val.ToString("N0");
                    }
                }

                // combo boxes
                byte.TryParse(mf.Tls.LoadProperty("GPSreceiver"), out tmp);
                cbReceiver.SelectedIndex = tmp;

                byte.TryParse(mf.Tls.LoadProperty("IMU"), out tmp);
                cbIMU.SelectedIndex = tmp;

                // check boxes
                for (int i = 0; i < CKs.Length; i++)
                {
                    bool.TryParse(mf.Tls.LoadProperty(CKs[i].Name), out Checked);
                    CKs[i].Checked = Checked;
                }
            }
            catch (Exception ex)
            {
                mf.Tls.ShowHelp(ex.Message, this.Text, 3000, true);
            }
        }

        private void SaveSettings()
        {
            try
            {
                // textboxes
                for (int i = 0; i < CFG.Length; i++)
                {
                    mf.Tls.SaveProperty(CFG[i].Name, CFG[i].Text);
                }

                // combo boxes
                mf.Tls.SaveProperty("GPSreceiver", cbReceiver.SelectedIndex.ToString());
                mf.Tls.SaveProperty("IMU", cbIMU.SelectedIndex.ToString());

                // check boxes
                for (int i = 0; i < CKs.Length; i++)
                {
                    mf.Tls.SaveProperty(CKs[i].Name, CKs[i].Checked.ToString());
                }
            }
            catch (Exception ex)
            {
                mf.Tls.ShowHelp(ex.Message, this.Text, 3000, true);
            }
        }

        private void SetButtons(bool Edited)
        {
            if (!Initializing)
            {
                if (Edited)
                {
                    btnCancel.Enabled = true;
                    this.bntOK.Text = Lang.lgSave;
                    btnSendToModule.Enabled = false;
                    TabEdited[tabControl1.SelectedIndex] = true;
                }
                else
                {
                    btnCancel.Enabled = false;
                    this.bntOK.Text = Lang.lgClose;
                    btnSendToModule.Enabled = true;
                }
            }
        }

        private void tb_Enter(object sender, EventArgs e)
        {
            int index = (int)((TextBox)sender).Tag;
            int tmp;
            int max;
            int min;

            switch (index)
            {
                case 0:
                case 1:
                    max = 200;
                    min = 0;
                    break;

                case 2:
                case 5:
                case 6:
                    max = 10000;
                    min = 0;
                    break;

                case 3:
                case 4:
                    max = 30;
                    min = 0;
                    break;

                case 8:
                    max = 255;
                    min = 0;
                    break;

                case 9:
                    max = 255;
                    min = 1;
                    break;

                case 7:
                    max = 4;
                    min = 1;
                    break;

                case 10:
                    max = 8;
                    min = 1;
                    break;

                default:
                    // teensy pins
                    max = 41;
                    min = 0;
                    break;
            }

            int.TryParse(CFG[index].Text, out tmp);
            using (var form = new FormNumeric(min, max, tmp))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (index == 5)
                    {
                        // pulse cal, 1 decimal
                        CFG[index].Text = form.ReturnValue.ToString("N1");
                    }
                    else
                    {
                        CFG[index].Text = form.ReturnValue.ToString();
                    }
                }
            }
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            SetButtons(true);
        }

        private void tb_Validating(object sender, CancelEventArgs e)
        {
            int index = (int)((TextBox)sender).Tag;
            int tmp;
            int max;
            int min;

            switch (index)
            {
                case 0:
                case 1:
                    max = 200;
                    min = 0;
                    break;

                case 2:
                case 5:
                case 6:
                    max = 10000;
                    min = 0;
                    break;

                case 3:
                case 4:
                    max = 30;
                    min = 0;
                    break;

                case 8:
                    max = 255;
                    min = 0;
                    break;

                case 9:
                    max = 255;
                    min = 1;
                    break;

                case 7:
                    max = 4;
                    min = 1;
                    break;

                case 10:
                    max = 8;
                    min = 1;
                    break;

                default:
                    // teensy pins
                    max = 41;
                    min = 0;
                    break;
            }
            int.TryParse(CFG[index].Text, out tmp);
            if (tmp < min || tmp > max)
            {
                System.Media.SystemSounds.Exclamation.Play();
                e.Cancel = true;
            }
        }

        private void UpdateForm()
        {
            Initializing = true;
            LoadSettings();
            Initializing = false;
        }
    }
}