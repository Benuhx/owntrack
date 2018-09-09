using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace owntrack
{
    public partial class OwntrackData
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("acc")]
        public long Acc { get; set; }

        [JsonProperty("alt")]
        public double Alt { get; set; }

        [JsonProperty("batt")]
        public long Batt { get; set; }

        [JsonProperty("conn")]
        public string Conn { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("tst")]
        public long Tst { get; set; }

        [JsonProperty("vac")]
        public long Vac { get; set; }

        [JsonProperty("vel")]
        public double Vel { get; set; }
    }
}
