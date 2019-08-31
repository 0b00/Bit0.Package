using Newtonsoft.Json;
using System;

namespace Bit0.Registry.Core
{
    public class PackageVersion
    {
        [JsonProperty("version")]
        public String Version { get; set; }
        [JsonProperty("url")]
        public String Url { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        [JsonProperty("size")]
        public Int64 Size { get; set; }
        [JsonProperty("sha256")]
        public String Sha256 { get; set; } // TODO: Hash check
    }
}