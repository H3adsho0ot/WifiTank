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
using System.IO;
using System.Reflection;
using WebSocketSharp.Server;
using System.Net;

namespace VisualTankControl
{
    public partial class FrmMain : Form
    {
        private WebSocketServer _wsServer = null;
        private static WebSocket _webSocket = null;
        private static Chassis _chassis;

        private static int _maxSpeed = 100;
        private static int _minSpeed = 0;

        public static int _gamepadY = 0;
        private static int _gamepadRotationZ = 0;
        public static int _gamepadZ = 0;
        private static int _gamepadX = 0;

        private static string _lastJson = string.Empty;

        public FrmMain()
        {
            InitializeComponent();
        }

        private static void sendJson()
        {
            ASCIIEncoding enc = new ASCIIEncoding();
            string jsonObject = JsonConvert.SerializeObject(_chassis);

            if (jsonObject != _lastJson)
            {
                if (_webSocket != null && _webSocket.IsAlive)
                {
                    _webSocket.Send(jsonObject);
                }
            }

            _lastJson = jsonObject;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _chassis = new Chassis();

            tbTankMaxSpeed.Value = _maxSpeed;
            lblTankMaxSpeedVal.Text = _maxSpeed.ToString();
            tbTankMinSpeed.Value = _minSpeed;
            lblTankMinSpeedVal.Text = _minSpeed.ToString();

            txtWebsocket.Text = "192.168.178.56:8080";
        }

        private void OnGamepadInput(JoystickUpdate update)
        {
            if (update.Offset == JoystickOffset.Y)
            {
                _gamepadY = update.Value;
                manageControllerInput();
            }
            if (update.Offset == JoystickOffset.RotationZ)
            {
                _gamepadRotationZ = update.Value;
                manageControllerInput();
            }
            if (update.Offset == JoystickOffset.Z)
            {
                _gamepadZ = update.Value;
                manageControllerInput();
            }
            if (update.Offset == JoystickOffset.X)
            {
                _gamepadX = update.Value;
                manageControllerInput();
            }
        }

        public static void manageControllerInput()
        {
            _chassis.rightChainSpeed = _gamepadY;
            _chassis.leftChainSpeed = _gamepadY;
            if (_gamepadY < 0)
            {
                _chassis.leftChainSpeed = _chassis.leftChainSpeed * -1;
                _chassis.leftChainForward = false;
                _chassis.rightChainSpeed = _chassis.rightChainSpeed * -1;
                _chassis.rightChainForward = false;
            }
            else
            {
                _chassis.leftChainForward = true;
                _chassis.rightChainForward = true;
            }

            if (_gamepadZ > 0)
            {
                _chassis.leftChainSpeed = _chassis.leftChainSpeed * (100 - _gamepadZ) / 100;
                _chassis.rightChainSpeed = _chassis.rightChainSpeed * (100 + _gamepadZ) / 100;
            }
            else
            {
                _chassis.rightChainSpeed = _chassis.rightChainSpeed * (100 - _gamepadZ * -1) / 100;
                _chassis.leftChainSpeed = _chassis.leftChainSpeed * (100 + _gamepadZ * -1) / 100;
            }

            if (_chassis.leftChainSpeed > 100)
            {
                _chassis.leftChainSpeed = 100;
            }
            else if (_chassis.leftChainSpeed < 0)
            {
                _chassis.leftChainSpeed = 0;
            }

            if (_chassis.rightChainSpeed > 100)
            {
                _chassis.rightChainSpeed = 100;
            }
            else if (_chassis.rightChainSpeed < 0)
            {
                _chassis.rightChainSpeed = 0;
            }

            if (_chassis.leftChainSpeed > 0)
            {
                _chassis.leftChainSpeed = remap(_chassis.leftChainSpeed, 0, 100, _minSpeed, _maxSpeed);
            }
            else
            {
                _chassis.leftChainSpeed = 0;
            }

            if (_chassis.rightChainSpeed > 0)
            {
                _chassis.rightChainSpeed = remap(_chassis.rightChainSpeed, 0, 100, _minSpeed, _maxSpeed);
            }
            else
            {
                _chassis.rightChainSpeed = 0;
            }

            sendJson();
        }

        private void tbTankMaxSpeed_Scroll(object sender, EventArgs e)
        {
            _maxSpeed = tbTankMaxSpeed.Value;
            lblTankMaxSpeedVal.Text = _maxSpeed.ToString();
        }

        public static int remap(float from, float fromMin, float fromMax, float toMin, float toMax)
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
            _webSocket.OnMessage += new EventHandler<MessageEventArgs>(webSocketOnMessage);
        }

        private void webSocketOnMessage(object test, MessageEventArgs args)
        {
            byte[] newByte = ToByteArray(args.Data);
            MemoryStream memStream = new MemoryStream(newByte);

            Image image = Bitmap.FromStream(memStream);

            //SetControlPropertyThreadSafe(pictureBox1, "Width", image.Width);
            //SetControlPropertyThreadSafe(pictureBox1, "Height", image.Height);

            image.RotateFlip(RotateFlipType.Rotate90FlipNone);

            if (_wsServer != null && _wsServer.IsListening)
            {
                _wsServer.WebSocketServices.Broadcast(imageToByteArray(image));
            }

            //pictureBox1.Image = image;
            SetControlPropertyThreadSafe(pictureBox1, "Image", image);
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static byte[] ToByteArray(String HexString)
        {
            HexString = HexString.Replace("-", "");
            int NumberChars = HexString.Length;

            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        private void websocketDiconnect_Click(object sender, EventArgs e)
        {
            _webSocket.Close();
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

        private void btnServerStart_Click(object sender, EventArgs e)
        {
            _wsServer = new WebSocketServer(IPAddress.Any, 7070);
            _wsServer.AddWebSocketService<wsServer>("/");
            _wsServer.Start();
        }

        private void btnGamepadStart_Click(object sender, EventArgs e)
        {
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
        }
    }
}
