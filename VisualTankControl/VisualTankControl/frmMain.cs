using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualTankControl
{
    public partial class frmMain : Form
    {
        private SerialPort serialPort;
        private Chassis chassis;
        private int speed = 60;

        public frmMain()
        {
            InitializeComponent();            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            { 
                case Keys.Up:
                    chassis.leftChainForward = true;
                    chassis.rightChainForward = true;
                    chassis.leftChainSpeed = speed;
                    chassis.rightChainSpeed = speed;

                    sendJson();
                    break;
                case Keys.Down:
                    chassis.leftChainForward = false;
                    chassis.rightChainForward = false;
                    chassis.leftChainSpeed = speed;
                    chassis.rightChainSpeed = speed;

                    sendJson();
                    break;
                case Keys.Right:
                    chassis.leftChainForward = false;
                    chassis.rightChainForward = true;
                    chassis.leftChainSpeed = speed;
                    chassis.rightChainSpeed = speed;

                    sendJson();
                    break;
                case Keys.Left:
                    chassis.leftChainForward = true;
                    chassis.rightChainForward = false;
                    chassis.leftChainSpeed = speed;
                    chassis.rightChainSpeed = speed;

                    sendJson();
                    break;
            }
        }

        private void sendJson()
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] data = enc.GetBytes(JsonConvert.SerializeObject(chassis) + Environment.NewLine);
            serialPort.Write(data, 0, data.Length);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                chassis.leftChainSpeed = 0;
                chassis.rightChainSpeed = 0;

                sendJson();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void btnSerialOpen_Click(object sender, EventArgs e)
        {
            if(!serialPort.IsOpen)
            {
                serialPort.PortName = cmbSerial.SelectedItem.ToString();
                serialPort.Open();

                btnSerialOpen.Enabled = false;
                btnSerialClose.Enabled = true;
                cmbSerial.Enabled = false;
                btnSerialUpdate.Enabled = false;
            }
        }

        private void btnSerialClose_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Open();

                btnSerialOpen.Enabled = true;
                btnSerialClose.Enabled = false;
                cmbSerial.Enabled = true;
                btnSerialUpdate.Enabled = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            serialPort = new SerialPort();
            serialPort.BaudRate = 1000000;

            cmbSerial.Items.AddRange(SerialPort.GetPortNames());
            cmbSerial.SelectedItem = cmbSerial.Items[0];

            chassis = new Chassis();

            tbTankMaxSpeed.Value = speed;
        }

        private void tbTankMaxSpeed_Scroll(object sender, EventArgs e)
        {
            speed = tbTankMaxSpeed.Value;
        }
    }
}
