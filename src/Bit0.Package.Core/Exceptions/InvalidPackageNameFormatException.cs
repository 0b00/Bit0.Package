using Bit0.Package.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Bit0.Package.Core.Exceptions
{
    public class InvalidPackageNameFormatException : Exception
    {
        public EventId EventId => LogEvents.InvalidPackName;

        public InvalidPackageNameFormatException(String message, Exception innerException = null) : base(message, innerException)
        { }
    }
}
