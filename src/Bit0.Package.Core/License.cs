﻿using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Bit0.Package.Core
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