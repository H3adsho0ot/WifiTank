namespace VisualTankControl
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnSerialOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSerialUpdate = new System.Windows.Forms.Button();
            this.cmbSerial = new System.Windows.Forms.ComboBox();
            this.btnSerialClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTankMinSpeedVal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTankMinSpeed = new System.Windows.Forms.TrackBar();
            this.lblTankMaxSpeedVal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTankMaxSpeed = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWebsocket = new System.Windows.Forms.TextBox();
            this.websocketDiconnect = new System.Windows.Forms.Button();
            this.websocketConnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMinSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMaxSpeed)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSerialOpen
            // 
            this.btnSerialOpen.Location = new System.Drawing.Point(164, 18);
            this.btnSerialOpen.Name = "btnSerialOpen";
            this.btnSerialOpen.Size = new System.Drawing.Size(75, 22);
            this.btnSerialOpen.TabIndex = 0;
            this.btnSerialOpen.Text = "Open";
            this.btnSerialOpen.UseVisualStyleBackColor = true;
            this.btnSerialOpen.Click += new System.EventHandler(this.btnSerialOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSerialUpdate);
            this.groupBox1.Controls.Add(this.cmbSerial);
            this.groupBox1.Controls.Add(this.btnSerialClose);
            this.groupBox1.Controls.Add(this.btnSerialOpen);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 53);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial";
            // 
            // btnSerialUpdate
            // 
            this.btnSerialUpdate.Location = new System.Drawing.Point(9, 19);
            this.btnSerialUpdate.Name = "btnSerialUpdate";
            this.btnSerialUpdate.Size = new System.Drawing.Size(22, 22);
            this.btnSerialUpdate.TabIndex = 3;
            this.btnSerialUpdate.Text = "↻";
            this.btnSerialUpdate.UseVisualStyleBackColor = true;
            this.btnSerialUpdate.Click += new System.EventHandler(this.btnSerialUpdate_Click);
            // 
            // cmbSerial
            // 
            this.cmbSerial.FormattingEnabled = true;
            this.cmbSerial.Location = new System.Drawing.Point(37, 19);
            this.cmbSerial.Name = "cmbSerial";
            this.cmbSerial.Size = new System.Drawing.Size(121, 21);
            this.cmbSerial.TabIndex = 2;
            // 
            // btnSerialClose
            // 
            this.btnSerialClose.Enabled = false;
            this.btnSerialClose.Location = new System.Drawing.Point(245, 18);
            this.btnSerialClose.Name = "btnSerialClose";
            this.btnSerialClose.Size = new System.Drawing.Size(75, 22);
            this.btnSerialClose.TabIndex = 1;
            this.btnSerialClose.Text = "Close";
            this.btnSerialClose.UseVisualStyleBackColor = true;
            this.btnSerialClose.Click += new System.EventHandler(this.btnSerialClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTankMinSpeedVal);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbTankMinSpeed);
            this.groupBox2.Controls.Add(this.lblTankMaxSpeedVal);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbTankMaxSpeed);
            this.groupBox2.Location = new System.Drawing.Point(12, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 131);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tank";
            // 
            // lblTankMinSpeedVal
            // 
            this.lblTankMinSpeedVal.AutoSize = true;
            this.lblTankMinSpeedVal.Location = new System.Drawing.Point(6, 83);
            this.lblTankMinSpeedVal.Name = "lblTankMinSpeedVal";
            this.lblTankMinSpeedVal.Size = new System.Drawing.Size(35, 13);
            this.lblTankMinSpeedVal.TabIndex = 8;
            this.lblTankMinSpeedVal.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Min Speed:";
            // 
            // tbTankMinSpeed
            // 
            this.tbTankMinSpeed.Location = new System.Drawing.Point(76, 70);
            this.tbTankMinSpeed.Maximum = 100;
            this.tbTankMinSpeed.Name = "tbTankMinSpeed";
            this.tbTankMinSpeed.Size = new System.Drawing.Size(244, 45);
            this.tbTankMinSpeed.TabIndex = 6;
            this.tbTankMinSpeed.Scroll += new System.EventHandler(this.tbTankMinSpeed_Scroll);
            // 
            // lblTankMaxSpeedVal
            // 
            this.lblTankMaxSpeedVal.AutoSize = true;
            this.lblTankMaxSpeedVal.Location = new System.Drawing.Point(6, 32);
            this.lblTankMaxSpeedVal.Name = "lblTankMaxSpeedVal";
            this.lblTankMaxSpeedVal.Size = new System.Drawing.Size(35, 13);
            this.lblTankMaxSpeedVal.TabIndex = 5;
            this.lblTankMaxSpeedVal.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max Speed:";
            // 
            // tbTankMaxSpeed
            // 
            this.tbTankMaxSpeed.Location = new System.Drawing.Point(76, 19);
            this.tbTankMaxSpeed.Maximum = 100;
            this.tbTankMaxSpeed.Name = "tbTankMaxSpeed";
            this.tbTankMaxSpeed.Size = new System.Drawing.Size(244, 45);
            this.tbTankMaxSpeed.TabIndex = 0;
            this.tbTankMaxSpeed.Scroll += new System.EventHandler(this.tbTankMaxSpeed_Scroll);
            this.tbTankMaxSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTankMaxSpeed_KeyDown);
            this.tbTankMaxSpeed.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbTankMaxSpeed_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWebsocket);
            this.groupBox3.Controls.Add(this.websocketDiconnect);
            this.groupBox3.Controls.Add(this.websocketConnect);
            this.groupBox3.Location = new System.Drawing.Point(12, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(330, 53);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "WebSocket";
            // 
            // txtWebsocket
            // 
            this.txtWebsocket.Location = new System.Drawing.Point(9, 20);
            this.txtWebsocket.Name = "txtWebsocket";
            this.txtWebsocket.Size = new System.Drawing.Size(146, 20);
            this.txtWebsocket.TabIndex = 2;
            // 
            // websocketDiconnect
            // 
            this.websocketDiconnect.Enabled = false;
            this.websocketDiconnect.Location = new System.Drawing.Point(245, 18);
            this.websocketDiconnect.Name = "websocketDiconnect";
            this.websocketDiconnect.Size = new System.Drawing.Size(75, 22);
            this.websocketDiconnect.TabIndex = 1;
            this.websocketDiconnect.Text = "Disconnect";
            this.websocketDiconnect.UseVisualStyleBackColor = true;
            this.websocketDiconnect.Click += new System.EventHandler(this.websocketDiconnect_Click);
            // 
            // websocketConnect
            // 
            this.websocketConnect.Location = new System.Drawing.Point(164, 18);
            this.websocketConnect.Name = "websocketConnect";
            this.websocketConnect.Size = new System.Drawing.Size(75, 22);
            this.websocketConnect.TabIndex = 0;
            this.websocketConnect.Text = "Connect";
            this.websocketConnect.UseVisualStyleBackColor = true;
            this.websocketConnect.Click += new System.EventHandler(this.websocketConnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 268);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(329, 237);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 519);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "VisualTankControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMinSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMaxSpeed)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSerialOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSerialClose;
        private System.Windows.Forms.ComboBox cmbSerial;
        private System.Windows.Forms.Button btnSerialUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbTankMaxSpeed;
        private System.Windows.Forms.Label lblTankMaxSpeedVal;
        private System.Windows.Forms.Label lblTankMinSpeedVal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbTankMinSpeed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtWebsocket;
        private System.Windows.Forms.Button websocketDiconnect;
        private System.Windows.Forms.Button websocketConnect;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

