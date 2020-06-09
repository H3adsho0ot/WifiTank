using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace VisualTankControl
{
    public class wsServer : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            try
            {
                GamePadInput input = JsonConvert.DeserializeObject<GamePadInput>(e.Data);

                FrmMain._gamepadY = input.YAxis;
                FrmMain._gamepadZ = input.ZAxis;

                FrmMain.manageControllerInput();
            }
            catch
            {
                FrmMain._gamepadY = 0;
                FrmMain._gamepadZ = 0;
            }
            //Sessions.Broadcast("Jo");
        }
    }
}
