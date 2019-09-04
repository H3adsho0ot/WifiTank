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
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private Chassis chassis;
        private int speed = 50;

        public Form1()
        {
            InitializeComponent();
            serialPort = new SerialPort();
            serialPort.BaudRate = 1000000;
            serialPort.PortName = "COM8";

            serialPort.Open();

            chassis = new Chassis();           
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
            serialPort.Close();
        }
    }
}
