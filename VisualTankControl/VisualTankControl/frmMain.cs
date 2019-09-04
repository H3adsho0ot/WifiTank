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
        private SerialPort _serialPort;
        private Chassis _chassis;
        private int _maxSpeed = 60;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    _chassis.leftChainForward = true;
                    _chassis.rightChainForward = true;
                    _chassis.leftChainSpeed = _maxSpeed;
                    _chassis.rightChainSpeed = _maxSpeed;

                    sendJson();
                    break;
                case Keys.S:
                    _chassis.leftChainForward = false;
                    _chassis.rightChainForward = false;
                    _chassis.leftChainSpeed = _maxSpeed;
                    _chassis.rightChainSpeed = _maxSpeed;

                    sendJson();
                    break;
                case Keys.D:
                    _chassis.leftChainForward = false;
                    _chassis.rightChainForward = true;
                    _chassis.leftChainSpeed = _maxSpeed;
                    _chassis.rightChainSpeed = _maxSpeed;

                    sendJson();
                    break;
                case Keys.A:
                    _chassis.leftChainForward = true;
                    _chassis.rightChainForward = false;
                    _chassis.leftChainSpeed = _maxSpeed;
                    _chassis.rightChainSpeed = _maxSpeed;

                    sendJson();
                    break;
            }
        }

        private void sendJson()
        {
            if (_serialPort.IsOpen)
            {
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] data = enc.GetBytes(JsonConvert.SerializeObject(_chassis) + Environment.NewLine);
                _serialPort.Write(data, 0, data.Length);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S || e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                _chassis.leftChainSpeed = 0;
                _chassis.rightChainSpeed = 0;

                sendJson();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        private void btnSerialOpen_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = cmbSerial.SelectedItem.ToString();
                _serialPort.Open();

                btnSerialOpen.Enabled = false;
                btnSerialClose.Enabled = true;
                cmbSerial.Enabled = false;
                btnSerialUpdate.Enabled = false;
            }
        }

        private void btnSerialClose_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Open();

                btnSerialOpen.Enabled = true;
                btnSerialClose.Enabled = false;
                cmbSerial.Enabled = true;
                btnSerialUpdate.Enabled = true;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _serialPort = new SerialPort();
            _serialPort.BaudRate = 1000000;

            cmbSerial.Items.AddRange(SerialPort.GetPortNames());
            cmbSerial.SelectedItem = cmbSerial.Items[0];

            _chassis = new Chassis();

            tbTankMaxSpeed.Value = _maxSpeed;
            lblTankMaxSpeedVal.Text = _maxSpeed.ToString();
        }

        private void tbTankMaxSpeed_Scroll(object sender, EventArgs e)
        {
            _maxSpeed = tbTankMaxSpeed.Value;
            lblTankMaxSpeedVal.Text = _maxSpeed.ToString();
        }

        private void btnSerialUpdate_Click(object sender, EventArgs e)
        {
            cmbSerial.Items.Clear();
            cmbSerial.Items.AddRange(SerialPort.GetPortNames());
            cmbSerial.SelectedItem = cmbSerial.Items[0];
        }
    }
}
