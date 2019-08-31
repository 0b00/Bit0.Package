using Microsoft.Extensions.Logging;
using System;

namespace Bit0.Registry.Core.Exceptions
{
    public class InvalidFeedException : Exception
    {
        public EventId EventId => new EventId(3001, "InvalidFeed");

        public InvalidFeedException(Uri url, Exception innerException) : base(url.ToString(), innerException)
        {
        }
    }
}
