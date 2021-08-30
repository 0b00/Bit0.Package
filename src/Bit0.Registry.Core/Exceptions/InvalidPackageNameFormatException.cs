using Bit0.Registry.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Bit0.Registry.Core.Exceptions
{
    public class InvalidPackageNameFormatException : Exception
    {
        public EventId EventId => LogEvents.InvalidPackName;

        public InvalidPackageNameFormatException(String message, Exception innerException = null) : base(message, innerException)
        { }
    }
}
