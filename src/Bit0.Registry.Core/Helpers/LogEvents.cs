using Microsoft.Extensions.Logging;

namespace Bit0.Registry.Core.Helpers
{
    public static class LogEvents
    {
        public static EventId DownloadedPack => new EventId(3101, "DownloadedPackFile");
        public static EventId ReadPack => new EventId(3102, "ParsedPackFile");
        public static EventId ExtractedPack => new EventId(3103, "ExtractedPackFile");
        public static EventId InvalidPackName => new EventId(3111, "InvalidPackageName");
        public static EventId InvalidPackFile => new EventId(3112, "InvalidPackFileSource");
    }
}
