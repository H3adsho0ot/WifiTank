using Newtonsoft.Json;

namespace VisualTankControl
{
    public class Chassis
    {
        [JsonProperty("LCS")]
        public int leftChainSpeed { get; set; }

        [JsonProperty("LCF")]
        public bool leftChainForward { get; set; }

        [JsonProperty("RCS")]
        public int rightChainSpeed { get; set; }

        [JsonProperty("RCF")]
        public bool rightChainForward { get; set; }
    }
}
