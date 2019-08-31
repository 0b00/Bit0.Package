using Microsoft.Extensions.Logging;
using System;

namespace Bit0.Registry.Core.Exceptions
{
    public class InvalidPackFileException : Exception
    {
        public EventId EventId => new EventId(3002, "InvalidPackFile");

        public InvalidPackFileException(String message, Exception innerException) : base(message, innerException)
        {
        }
    }
}