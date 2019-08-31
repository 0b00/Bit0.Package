using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Bit0.Registry.Core.Extensions
{
    public static class StreamExtensions
    {
        public static JsonTextReader OpenJsonReader(this ZipArchiveEntry entry)
        {
            return entry.Open().GetJsonReader();
        }

        public static JsonTextReader GetJsonReader(this Stream stream)
        {
            return new JsonTextReader(new StreamReader(stream));
        }
    }
}
