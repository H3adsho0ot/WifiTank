using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualTankControl
{
    public class GamePadInput
    {
        [JsonProperty("Y")]
        public int YAxis { get; set; }

        [JsonProperty("Z")]
        public int ZAxis { get; set; }
    }
}
