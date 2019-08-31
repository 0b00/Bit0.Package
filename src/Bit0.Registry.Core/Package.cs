using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Bit0.Registry.Core
{
    public class Package
    {
        [JsonProperty("id")]
        public String Id { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PackageType Type { get; set; }
        [JsonProperty("published")]
        public DateTime Published { get; set; }
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
        [JsonProperty("license")]
        public License License { get; set; }
        [JsonProperty("author")]
        public Author Author { get; set; }
        [JsonProperty("homepage")]
        public String Homepage { get; set; }
        [JsonProperty("icon")]
        public String Icon { get; set; }
        [JsonProperty("screenshot")]
        public String Screenshot { get; set; }
        [JsonProperty("tags")]
        public IEnumerable<String> Tags { get; set; }
        [JsonProperty("features")]
        public IEnumerable<String> Features { get; set; }
        [JsonProperty("deps")]
        public IDictionary<String, String> Dependencies { get; set; }
        [JsonProperty("versions")]
        public IEnumerable<PackageVersion> Versions { get; set; }

        public override String ToString() {
            return $"[{Id}] {Name}";
        }
    }
}
