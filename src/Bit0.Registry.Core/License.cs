using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Bit0.Registry.Core
{
    [ExcludeFromCodeCoverage]
    public class License
    {
        [JsonProperty("name")]
        public String Name { get; set; }
        
        [JsonProperty("url")]
        public String Url { get; set; }
    }
}