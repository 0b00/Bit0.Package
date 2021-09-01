using Newtonsoft.Json;

namespace Bit0.Package.Core
{
    /// <summary>
    /// Pack meta data
    /// </summary>
    public class Pack : IPack
    {
        [JsonIgnore]
        public FileInfo PackFile { get; set; }

        [JsonProperty("id")]
        public String Id { get; set; } = Guid.NewGuid().ToString("D");

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
        public IEnumerable<String> Dependencies { get; set; }
    }
}