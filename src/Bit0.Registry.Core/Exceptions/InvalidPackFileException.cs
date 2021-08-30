using Bit0.Registry.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Bit0.Registry.Core.Exceptions
{
    public class InvalidPackFileException : Exception
    {
        public EventId EventId => LogEvents.InvalidPackFile;

        public InvalidPackFileException(String message, Exception innerException = null) : base(message, innerException)
        { }
    }
}