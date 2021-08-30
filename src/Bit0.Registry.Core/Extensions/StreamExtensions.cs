using Newtonsoft.Json;
using System.IO.Compression;

namespace Bit0.Registry.Core.Extensions
{
    public static class StreamExtensions
    {
        public static JsonTextReader OpenJsonReader(this ZipArchiveEntry entry)
        {
            return entry.Open().GetJsonReader();
        }

        //public static JsonTextReader OpenJsonReader(this FileInfo entry)
        //{
        //    return entry.OpenRead().GetJsonReader();
        //}

        public static JsonTextReader GetJsonReader(this Stream stream)
        {
            return new JsonTextReader(new StreamReader(stream));
        }
    }
}
