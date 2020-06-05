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
using System.Threading;
using System.Diagnostics;
using WebSocketSharp;
using SharpDX.DirectInput;

namespace VisualTankControl
{
    public partial class FrmMain : Form
    {
        private SerialPort _serialPort;
        private WebSocket _webSocket = null;
        private Chassis _chassis;

        private int _maxSpeed = 90;
        private int _minSpeed = 65;

        private XInputController _controller;
        private int _controllerRefreshRate = 60;
        private System.Threading.Timer _controllerTimer;

        private int GamepadY = 0;
        private int GamepadRotationZ = 0;

        public FrmMain()
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
            ASCIIEncoding enc = new ASCIIEncoding();
            string jsonObject = JsonConvert.SerializeObject(_chassis);

            Debug.WriteLine(JsonConvert.SerializeObject(_chassis) + Environment.NewLine);

            if (_serialPort.IsOpen)
            {
                byte[] data = enc.GetBytes(jsonObject + Environment.NewLine);
                _serialPort.Write(data, 0, data.Length);
            }
            else if (_webSocket != null && _webSocket.IsAlive)
            {
                _webSocket.Send(jsonObject);
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
                _serialPort.Close();

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
            tbTankMinSpeed.Value = _minSpeed;
            lblTankMinSpeedVal.Text = _minSpeed.ToString();

            //_controller = new XInputController();
            //_controllerTimer = new System.Threading.Timer(obj => manageControllerInput());
            //_controllerTimer.Change(0, 1000 / _controllerRefreshRate);

            // Find a Joystick Guid
            var joystickGuid = Guid.Empty;
            DirectInput directInput = new DirectInput();
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid = deviceInstance.InstanceGuid;
            }
            if (joystickGuid != Guid.Empty)
            {
                Gamepad gamepad = new Gamepad(joystickGuid);
                gamepad.RaiseGamepadInput += new Gamepad.GamepadInput(OnGamepadInput);
                gamepad.StartPoll();
            }

            txtWebsocket.Text = "192.168.178.56:80";
        }

        private void OnGamepadInput(JoystickUpdate update)
        {
            if (update.Offset == JoystickOffset.Y)
            {
                GamepadY = update.Value;
                //Console.WriteLine(GamepadY);
                manageControllerInput(GamepadY, GamepadRotationZ);
            }
            if (update.Offset == JoystickOffset.RotationZ)
            {
                GamepadRotationZ = update.Value;
                //Console.WriteLine(GamepadZ);
                manageControllerInput(GamepadY, GamepadRotationZ);
            }
        }

        private void manageControllerInput(int y, int z)
        {
            _chassis.leftChainSpeed = y;
            if (y < 0)
            {
                _chassis.leftChainSpeed = _chassis.leftChainSpeed * -1;
                _chassis.leftChainForward = false;
            }
            else
            {
                _chassis.leftChainForward = true;
            }
            _chassis.leftChainSpeed = remap(_chassis.leftChainSpeed, 0, 100, _minSpeed, _maxSpeed);
            if (y == 0)
            {
                _chassis.leftChainSpeed = 0;
            }

            _chassis.rightChainSpeed = z;
            if (z < 0)
            {
                _chassis.rightChainSpeed = _chassis.rightChainSpeed * -1;
                _chassis.rightChainForward = false;
            }
            else
            {
                _chassis.rightChainForward = true;
            }
            _chassis.rightChainSpeed = remap(_chassis.rightChainSpeed, 0, 100, _minSpeed, _maxSpeed);
            if (z == 0)
            {
                _chassis.rightChainSpeed = 0;
            }

            sendJson();
        }

        //private void manageControllerInput()
        //{
        //    if(_controller.connected)
        //    {
        //        _controller.Update();

        //        _chassis.leftChainSpeed = _controller.leftThumb.Y;
        //        if (_controller.leftThumb.Y < 0)
        //        {
        //            _chassis.leftChainSpeed = _chassis.leftChainSpeed * -1;
        //            _chassis.leftChainForward = false;
        //        }
        //        else
        //        {
        //            _chassis.leftChainForward = true;
        //        }
        //        _chassis.leftChainSpeed = remap(_chassis.leftChainSpeed, 0, 100, _minSpeed, _maxSpeed);
        //        if (_controller.leftThumb.Y == 0)
        //        {
        //            _chassis.leftChainSpeed = 0;
        //        }

        //        _chassis.rightChainSpeed = _controller.rightThumb.Y;
        //        if (_controller.rightThumb.Y < 0)
        //        {
        //            _chassis.rightChainSpeed = _chassis.rightChainSpeed * -1;
        //            _chassis.rightChainForward = false;
        //        }
        //        else
        //        {
        //            _chassis.rightChainForward = true;
        //        }
        //        _chassis.rightChainSpeed = remap(_chassis.rightChainSpeed, 0, 100, _minSpeed, _maxSpeed);
        //        if (_controller.rightThumb.Y == 0)
        //        {
        //            _chassis.rightChainSpeed = 0;
        //        }

        //        sendJson();
        //    }
        //}

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

        private void tbTankMaxSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(sender, e);
        }

        private void tbTankMaxSpeed_KeyUp(object sender, KeyEventArgs e)
        {
            Form1_KeyUp(sender, e);
        }

        public int remap(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return (int)to;
        }

        private void tbTankMinSpeed_Scroll(object sender, EventArgs e)
        {
            _minSpeed = tbTankMinSpeed.Value;
            lblTankMinSpeedVal.Text = _minSpeed.ToString();
        }

        private void websocketConnect_Click(object sender, EventArgs e)
        {
            _webSocket = new WebSocket("ws://" + txtWebsocket.Text);
            _webSocket.Connect();
        }

        private void websocketDiconnect_Click(object sender, EventArgs e)
        {
            _webSocket.Close();
        }
    }
}
