using Bit0.Package.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Bit0.Package.Core.Exceptions
{
    public class InvalidPackFileException : Exception
    {
        public EventId EventId => LogEvents.InvalidPackFile;

        public InvalidPackFileException(String message, Exception innerException = null) : base(message, innerException)
        { }
    }
}