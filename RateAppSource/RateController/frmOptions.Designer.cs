﻿namespace RateController
{
    partial class frmOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.ckMetric = new System.Windows.Forms.CheckBox();
            this.lbDelaySeconds = new System.Windows.Forms.Label();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.lbDelay = new System.Windows.Forms.Label();
            this.lbOnSeconds = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.lbOnTime = new System.Windows.Forms.Label();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.lbPrimedSpeed = new System.Windows.Forms.Label();
            this.rbFrench = new System.Windows.Forms.RadioButton();
            this.rbRussian = new System.Windows.Forms.RadioButton();
            this.rbPolish = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ckWorkSwitch = new System.Windows.Forms.CheckBox();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ckLargeScreen = new System.Windows.Forms.CheckBox();
            this.ckDualAuto = new System.Windows.Forms.CheckBox();
            this.ckPressure = new System.Windows.Forms.CheckBox();
            this.ckScreenSwitches = new System.Windows.Forms.CheckBox();
            this.ckTransparent = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbSimSpeed = new System.Windows.Forms.TextBox();
            this.lbSimUnits = new System.Windows.Forms.Label();
            this.ckSimSpeed = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ckResume = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.rbNederlands = new System.Windows.Forms.RadioButton();
            this.rbHungarian = new System.Windows.Forms.RadioButton();
            this.rbEnglish = new System.Windows.Forms.RadioButton();
            this.rbDeustch = new System.Windows.Forms.RadioButton();
            this.tcOptions.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckMetric
            // 
            this.ckMetric.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckMetric.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckMetric.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckMetric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckMetric.Location = new System.Drawing.Point(77, 23);
            this.ckMetric.Name = "ckMetric";
            this.ckMetric.Size = new System.Drawing.Size(250, 34);
            this.ckMetric.TabIndex = 119;
            this.ckMetric.Text = "Metric Units";
            this.ckMetric.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckMetric.UseVisualStyleBackColor = true;
            this.ckMetric.CheckedChanged += new System.EventHandler(this.rbLarge_CheckedChanged);
            // 
            // lbDelaySeconds
            // 
            this.lbDelaySeconds.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbDelaySeconds.Location = new System.Drawing.Point(293, 95);
            this.lbDelaySeconds.Name = "lbDelaySeconds";
            this.lbDelaySeconds.Size = new System.Drawing.Size(89, 24);
            this.lbDelaySeconds.TabIndex = 334;
            this.lbDelaySeconds.Text = "seconds";
            this.lbDelaySeconds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDelay
            // 
            this.tbDelay.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDelay.Location = new System.Drawing.Point(222, 92);
            this.tbDelay.MaxLength = 8;
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(65, 30);
            this.tbDelay.TabIndex = 333;
            this.tbDelay.Text = "0";
            this.tbDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDelay.TextChanged += new System.EventHandler(this.tbTime_TextChanged);
            this.tbDelay.Enter += new System.EventHandler(this.tbDelay_Enter);
            this.tbDelay.Validating += new System.ComponentModel.CancelEventHandler(this.tbDelay_Validating);
            // 
            // lbDelay
            // 
            this.lbDelay.AutoSize = true;
            this.lbDelay.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDelay.Location = new System.Drawing.Point(57, 96);
            this.lbDelay.Name = "lbDelay";
            this.lbDelay.Size = new System.Drawing.Size(117, 23);
            this.lbDelay.TabIndex = 332;
            this.lbDelay.Text = "Switch Delay";
            // 
            // lbOnSeconds
            // 
            this.lbOnSeconds.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbOnSeconds.Location = new System.Drawing.Point(293, 17);
            this.lbOnSeconds.Name = "lbOnSeconds";
            this.lbOnSeconds.Size = new System.Drawing.Size(89, 24);
            this.lbOnSeconds.TabIndex = 331;
            this.lbOnSeconds.Text = "seconds";
            this.lbOnSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTime.Location = new System.Drawing.Point(222, 14);
            this.tbTime.MaxLength = 8;
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(65, 30);
            this.tbTime.TabIndex = 330;
            this.tbTime.Text = "0";
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbTime.TextChanged += new System.EventHandler(this.tbTime_TextChanged);
            this.tbTime.Enter += new System.EventHandler(this.tbTime_Enter);
            this.tbTime.Validating += new System.ComponentModel.CancelEventHandler(this.tbTime_Validating);
            // 
            // lbOnTime
            // 
            this.lbOnTime.AutoSize = true;
            this.lbOnTime.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOnTime.Location = new System.Drawing.Point(57, 18);
            this.lbOnTime.Name = "lbOnTime";
            this.lbOnTime.Size = new System.Drawing.Size(81, 23);
            this.lbOnTime.TabIndex = 329;
            this.lbOnTime.Text = "On Time";
            // 
            // lbSpeed
            // 
            this.lbSpeed.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbSpeed.Location = new System.Drawing.Point(293, 56);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(57, 24);
            this.lbSpeed.TabIndex = 328;
            this.lbSpeed.Text = "mph";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSpeed.Location = new System.Drawing.Point(222, 53);
            this.tbSpeed.MaxLength = 8;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(65, 30);
            this.tbSpeed.TabIndex = 327;
            this.tbSpeed.Text = "0";
            this.tbSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSpeed.TextChanged += new System.EventHandler(this.tbTime_TextChanged);
            this.tbSpeed.Enter += new System.EventHandler(this.tbSpeed_Enter);
            this.tbSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tbSpeed_Validating);
            // 
            // lbPrimedSpeed
            // 
            this.lbPrimedSpeed.AutoSize = true;
            this.lbPrimedSpeed.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrimedSpeed.Location = new System.Drawing.Point(57, 57);
            this.lbPrimedSpeed.Name = "lbPrimedSpeed";
            this.lbPrimedSpeed.Size = new System.Drawing.Size(63, 23);
            this.lbPrimedSpeed.TabIndex = 326;
            this.lbPrimedSpeed.Text = "Speed";
            // 
            // rbFrench
            // 
            this.rbFrench.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFrench.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbFrench.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbFrench.Location = new System.Drawing.Point(225, 95);
            this.rbFrench.Margin = new System.Windows.Forms.Padding(2);
            this.rbFrench.Name = "rbFrench";
            this.rbFrench.Size = new System.Drawing.Size(140, 34);
            this.rbFrench.TabIndex = 8;
            this.rbFrench.Tag = "6";
            this.rbFrench.Text = "French";
            this.rbFrench.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFrench.UseVisualStyleBackColor = true;
            // 
            // rbRussian
            // 
            this.rbRussian.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbRussian.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbRussian.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbRussian.Location = new System.Drawing.Point(225, 54);
            this.rbRussian.Margin = new System.Windows.Forms.Padding(2);
            this.rbRussian.Name = "rbRussian";
            this.rbRussian.Size = new System.Drawing.Size(140, 34);
            this.rbRussian.TabIndex = 7;
            this.rbRussian.Tag = "5";
            this.rbRussian.Text = "Russian";
            this.rbRussian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbRussian.UseVisualStyleBackColor = true;
            // 
            // rbPolish
            // 
            this.rbPolish.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPolish.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbPolish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbPolish.Location = new System.Drawing.Point(225, 11);
            this.rbPolish.Margin = new System.Windows.Forms.Padding(2);
            this.rbPolish.Name = "rbPolish";
            this.rbPolish.Size = new System.Drawing.Size(140, 34);
            this.rbPolish.TabIndex = 6;
            this.rbPolish.Tag = "4";
            this.rbPolish.Text = "Polish";
            this.rbPolish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPolish.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btnCancel.Image = global::RateController.Properties.Resources.Cancel64;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(299, 230);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 72);
            this.btnCancel.TabIndex = 124;
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btnOK.Image = global::RateController.Properties.Resources.OK;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(378, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 72);
            this.btnOK.TabIndex = 123;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ckWorkSwitch
            // 
            this.ckWorkSwitch.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckWorkSwitch.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckWorkSwitch.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckWorkSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckWorkSwitch.Location = new System.Drawing.Point(77, 77);
            this.ckWorkSwitch.Name = "ckWorkSwitch";
            this.ckWorkSwitch.Size = new System.Drawing.Size(250, 34);
            this.ckWorkSwitch.TabIndex = 125;
            this.ckWorkSwitch.Text = "Implement Work Switch";
            this.ckWorkSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckWorkSwitch.UseVisualStyleBackColor = true;
            this.ckWorkSwitch.CheckedChanged += new System.EventHandler(this.rbLarge_CheckedChanged);
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tabPage2);
            this.tcOptions.Controls.Add(this.tabPage1);
            this.tcOptions.Controls.Add(this.tabPage3);
            this.tcOptions.Controls.Add(this.tabPage4);
            this.tcOptions.Location = new System.Drawing.Point(12, 12);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(438, 216);
            this.tcOptions.TabIndex = 126;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ckLargeScreen);
            this.tabPage2.Controls.Add(this.ckDualAuto);
            this.tabPage2.Controls.Add(this.ckPressure);
            this.tabPage2.Controls.Add(this.ckScreenSwitches);
            this.tabPage2.Controls.Add(this.ckTransparent);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 179);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Display";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ckLargeScreen
            // 
            this.ckLargeScreen.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckLargeScreen.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckLargeScreen.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckLargeScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckLargeScreen.Location = new System.Drawing.Point(41, 22);
            this.ckLargeScreen.Name = "ckLargeScreen";
            this.ckLargeScreen.Size = new System.Drawing.Size(164, 34);
            this.ckLargeScreen.TabIndex = 127;
            this.ckLargeScreen.Text = "Large Screen";
            this.ckLargeScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckLargeScreen.UseVisualStyleBackColor = true;
            this.ckLargeScreen.CheckedChanged += new System.EventHandler(this.ckTransparent_CheckedChanged);
            // 
            // ckDualAuto
            // 
            this.ckDualAuto.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckDualAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckDualAuto.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckDualAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckDualAuto.Location = new System.Drawing.Point(41, 124);
            this.ckDualAuto.Name = "ckDualAuto";
            this.ckDualAuto.Size = new System.Drawing.Size(164, 34);
            this.ckDualAuto.TabIndex = 124;
            this.ckDualAuto.Text = "Dual Auto";
            this.ckDualAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckDualAuto.UseVisualStyleBackColor = true;
            this.ckDualAuto.CheckedChanged += new System.EventHandler(this.ckDualAuto_CheckedChanged);
            // 
            // ckPressure
            // 
            this.ckPressure.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckPressure.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckPressure.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckPressure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckPressure.Location = new System.Drawing.Point(218, 22);
            this.ckPressure.Name = "ckPressure";
            this.ckPressure.Size = new System.Drawing.Size(164, 34);
            this.ckPressure.TabIndex = 123;
            this.ckPressure.Text = "Pressure";
            this.ckPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckPressure.UseVisualStyleBackColor = true;
            this.ckPressure.CheckedChanged += new System.EventHandler(this.ckTransparent_CheckedChanged);
            // 
            // ckScreenSwitches
            // 
            this.ckScreenSwitches.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckScreenSwitches.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckScreenSwitches.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckScreenSwitches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckScreenSwitches.Location = new System.Drawing.Point(41, 74);
            this.ckScreenSwitches.Name = "ckScreenSwitches";
            this.ckScreenSwitches.Size = new System.Drawing.Size(164, 34);
            this.ckScreenSwitches.TabIndex = 122;
            this.ckScreenSwitches.Text = "Switches";
            this.ckScreenSwitches.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckScreenSwitches.UseVisualStyleBackColor = true;
            this.ckScreenSwitches.CheckedChanged += new System.EventHandler(this.ckTransparent_CheckedChanged);
            // 
            // ckTransparent
            // 
            this.ckTransparent.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckTransparent.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckTransparent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckTransparent.Location = new System.Drawing.Point(218, 74);
            this.ckTransparent.Name = "ckTransparent";
            this.ckTransparent.Size = new System.Drawing.Size(164, 34);
            this.ckTransparent.TabIndex = 119;
            this.ckTransparent.Text = "Transparent";
            this.ckTransparent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckTransparent.UseVisualStyleBackColor = true;
            this.ckTransparent.CheckedChanged += new System.EventHandler(this.ckTransparent_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbSimSpeed);
            this.tabPage1.Controls.Add(this.lbSimUnits);
            this.tabPage1.Controls.Add(this.ckSimSpeed);
            this.tabPage1.Controls.Add(this.ckMetric);
            this.tabPage1.Controls.Add(this.ckWorkSwitch);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 179);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Config";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbSimSpeed
            // 
            this.tbSimSpeed.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSimSpeed.Location = new System.Drawing.Point(276, 130);
            this.tbSimSpeed.MaxLength = 8;
            this.tbSimSpeed.Name = "tbSimSpeed";
            this.tbSimSpeed.Size = new System.Drawing.Size(65, 30);
            this.tbSimSpeed.TabIndex = 329;
            this.tbSimSpeed.Text = "0";
            this.tbSimSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSimSpeed.TextChanged += new System.EventHandler(this.tbSimSpeed_TextChanged);
            this.tbSimSpeed.Enter += new System.EventHandler(this.tbSimSpeed_Enter);
            this.tbSimSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.tbSimSpeed_Validating);
            // 
            // lbSimUnits
            // 
            this.lbSimUnits.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbSimUnits.Location = new System.Drawing.Point(347, 133);
            this.lbSimUnits.Name = "lbSimUnits";
            this.lbSimUnits.Size = new System.Drawing.Size(57, 24);
            this.lbSimUnits.TabIndex = 330;
            this.lbSimUnits.Text = "mph";
            this.lbSimUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckSimSpeed
            // 
            this.ckSimSpeed.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckSimSpeed.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckSimSpeed.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckSimSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckSimSpeed.Location = new System.Drawing.Point(28, 128);
            this.ckSimSpeed.Name = "ckSimSpeed";
            this.ckSimSpeed.Size = new System.Drawing.Size(242, 34);
            this.ckSimSpeed.TabIndex = 126;
            this.ckSimSpeed.Text = "Simulate Speed (No AOG)";
            this.ckSimSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckSimSpeed.UseVisualStyleBackColor = true;
            this.ckSimSpeed.CheckedChanged += new System.EventHandler(this.rbLarge_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ckResume);
            this.tabPage3.Controls.Add(this.lbDelaySeconds);
            this.tabPage3.Controls.Add(this.tbTime);
            this.tabPage3.Controls.Add(this.tbSpeed);
            this.tabPage3.Controls.Add(this.tbDelay);
            this.tabPage3.Controls.Add(this.lbPrimedSpeed);
            this.tabPage3.Controls.Add(this.lbSpeed);
            this.tabPage3.Controls.Add(this.lbDelay);
            this.tabPage3.Controls.Add(this.lbOnTime);
            this.tabPage3.Controls.Add(this.lbOnSeconds);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(430, 179);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Primed Start";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ckResume
            // 
            this.ckResume.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckResume.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckResume.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ckResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckResume.Location = new System.Drawing.Point(137, 131);
            this.ckResume.Name = "ckResume";
            this.ckResume.Size = new System.Drawing.Size(164, 34);
            this.ckResume.TabIndex = 335;
            this.ckResume.Text = "Resume";
            this.ckResume.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckResume.UseVisualStyleBackColor = true;
            this.ckResume.CheckedChanged += new System.EventHandler(this.ckTransparent_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.rbFrench);
            this.tabPage4.Controls.Add(this.rbNederlands);
            this.tabPage4.Controls.Add(this.rbRussian);
            this.tabPage4.Controls.Add(this.rbHungarian);
            this.tabPage4.Controls.Add(this.rbPolish);
            this.tabPage4.Controls.Add(this.rbEnglish);
            this.tabPage4.Controls.Add(this.rbDeustch);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(430, 179);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Language";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // rbNederlands
            // 
            this.rbNederlands.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbNederlands.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbNederlands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbNederlands.Location = new System.Drawing.Point(50, 136);
            this.rbNederlands.Margin = new System.Windows.Forms.Padding(2);
            this.rbNederlands.Name = "rbNederlands";
            this.rbNederlands.Size = new System.Drawing.Size(140, 34);
            this.rbNederlands.TabIndex = 9;
            this.rbNederlands.Tag = "3";
            this.rbNederlands.Text = "Nederlands";
            this.rbNederlands.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbNederlands.UseVisualStyleBackColor = true;
            // 
            // rbHungarian
            // 
            this.rbHungarian.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbHungarian.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbHungarian.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbHungarian.Location = new System.Drawing.Point(50, 95);
            this.rbHungarian.Margin = new System.Windows.Forms.Padding(2);
            this.rbHungarian.Name = "rbHungarian";
            this.rbHungarian.Size = new System.Drawing.Size(140, 34);
            this.rbHungarian.TabIndex = 8;
            this.rbHungarian.Tag = "2";
            this.rbHungarian.Text = "Hungarian";
            this.rbHungarian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbHungarian.UseVisualStyleBackColor = true;
            // 
            // rbEnglish
            // 
            this.rbEnglish.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbEnglish.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbEnglish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbEnglish.Location = new System.Drawing.Point(50, 54);
            this.rbEnglish.Margin = new System.Windows.Forms.Padding(2);
            this.rbEnglish.Name = "rbEnglish";
            this.rbEnglish.Size = new System.Drawing.Size(140, 34);
            this.rbEnglish.TabIndex = 7;
            this.rbEnglish.Tag = "1";
            this.rbEnglish.Text = "English";
            this.rbEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbEnglish.UseVisualStyleBackColor = true;
            // 
            // rbDeustch
            // 
            this.rbDeustch.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbDeustch.Checked = true;
            this.rbDeustch.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rbDeustch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbDeustch.Location = new System.Drawing.Point(50, 11);
            this.rbDeustch.Margin = new System.Windows.Forms.Padding(2);
            this.rbDeustch.Name = "rbDeustch";
            this.rbDeustch.Size = new System.Drawing.Size(140, 34);
            this.rbDeustch.TabIndex = 6;
            this.rbDeustch.TabStop = true;
            this.rbDeustch.Tag = "0";
            this.rbDeustch.Text = "Deustch";
            this.rbDeustch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbDeustch.UseVisualStyleBackColor = true;
            this.rbDeustch.CheckedChanged += new System.EventHandler(this.Language_CheckedChanged);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 303);
            this.Controls.Add(this.tcOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.Text = "Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmOptions_FormClosed);
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.tcOptions.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox ckMetric;
        private System.Windows.Forms.Label lbDelaySeconds;
        private System.Windows.Forms.TextBox tbDelay;
        private System.Windows.Forms.Label lbDelay;
        private System.Windows.Forms.Label lbOnSeconds;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label lbOnTime;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.Label lbPrimedSpeed;
        private System.Windows.Forms.RadioButton rbFrench;
        private System.Windows.Forms.RadioButton rbRussian;
        private System.Windows.Forms.RadioButton rbPolish;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox ckWorkSwitch;
        private System.Windows.Forms.TabControl tcOptions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RadioButton rbNederlands;
        private System.Windows.Forms.RadioButton rbHungarian;
        private System.Windows.Forms.RadioButton rbEnglish;
        private System.Windows.Forms.RadioButton rbDeustch;
        private System.Windows.Forms.CheckBox ckTransparent;
        private System.Windows.Forms.CheckBox ckPressure;
        private System.Windows.Forms.CheckBox ckScreenSwitches;
        private System.Windows.Forms.CheckBox ckSimSpeed;
        private System.Windows.Forms.TextBox tbSimSpeed;
        private System.Windows.Forms.Label lbSimUnits;
        private System.Windows.Forms.CheckBox ckDualAuto;
        private System.Windows.Forms.CheckBox ckLargeScreen;
        private System.Windows.Forms.CheckBox ckResume;
    }
}