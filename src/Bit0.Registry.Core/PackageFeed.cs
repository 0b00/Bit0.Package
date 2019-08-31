using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Bit0.Registry.Core
{
    public class PackageFeed
    {
        [JsonProperty("id")]
        public String Id { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        [JsonProperty("packages")]
        public IEnumerable<Package> Package { get; set; }
    }
}
