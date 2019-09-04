namespace VisualTankControl
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnSerialOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSerial = new System.Windows.Forms.ComboBox();
            this.btnSerialClose = new System.Windows.Forms.Button();
            this.btnSerialUpdate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbTankMaxSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMaxSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSerialOpen
            // 
            this.btnSerialOpen.Location = new System.Drawing.Point(164, 17);
            this.btnSerialOpen.Name = "btnSerialOpen";
            this.btnSerialOpen.Size = new System.Drawing.Size(75, 23);
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
            this.btnSerialClose.Location = new System.Drawing.Point(245, 17);
            this.btnSerialClose.Name = "btnSerialClose";
            this.btnSerialClose.Size = new System.Drawing.Size(75, 23);
            this.btnSerialClose.TabIndex = 1;
            this.btnSerialClose.Text = "Close";
            this.btnSerialClose.UseVisualStyleBackColor = true;
            this.btnSerialClose.Click += new System.EventHandler(this.btnSerialClose_Click);
            // 
            // btnSerialUpdate
            // 
            this.btnSerialUpdate.Location = new System.Drawing.Point(8, 19);
            this.btnSerialUpdate.Name = "btnSerialUpdate";
            this.btnSerialUpdate.Size = new System.Drawing.Size(23, 23);
            this.btnSerialUpdate.TabIndex = 3;
            this.btnSerialUpdate.Text = "↻";
            this.btnSerialUpdate.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tbTankMaxSpeed);
            this.groupBox2.Location = new System.Drawing.Point(12, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 78);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tank";
            // 
            // tbTankMaxSpeed
            // 
            this.tbTankMaxSpeed.Location = new System.Drawing.Point(76, 19);
            this.tbTankMaxSpeed.Maximum = 100;
            this.tbTankMaxSpeed.Name = "tbTankMaxSpeed";
            this.tbTankMaxSpeed.Size = new System.Drawing.Size(244, 45);
            this.tbTankMaxSpeed.TabIndex = 0;
            this.tbTankMaxSpeed.Scroll += new System.EventHandler(this.tbTankMaxSpeed_Scroll);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 157);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "VisualTankControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTankMaxSpeed)).EndInit();
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
    }
}

