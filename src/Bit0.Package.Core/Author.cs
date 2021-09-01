using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Bit0.Package.Core
{
    [ExcludeFromCodeCoverage]
    public class Author
    {
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("alias")]
        public String Alias { get; set; }
        [JsonProperty("email")]
        public String Email { get; set; }
        [JsonProperty("homepage")]
        public String HomePage { get; set; }
        [JsonProperty("social")]
        public IDictionary<String, String> Social { get; set; }

        public override String ToString()
        {
            return $"{Name} ({Alias})";
        }
    }
}