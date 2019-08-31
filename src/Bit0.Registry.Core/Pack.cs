using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bit0.Registry.Core
{
    public class Pack : IPack
    {
        [JsonIgnore]
        public FileInfo PackFile { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("version")]
        public String Version { get; set; }

        [JsonProperty("description")]
        public String Description { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("homepage")]
        public String Homepage { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<String> Tags { get; set; }

        [JsonProperty("features")]
        public IEnumerable<String> Features { get; set; }

        [JsonProperty("license")]
        public License License { get; set; }

        [JsonProperty("deps")]
        public IDictionary<String, String> Dependencies { get; set; }
    }
}