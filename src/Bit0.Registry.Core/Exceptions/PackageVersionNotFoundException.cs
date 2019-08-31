using Microsoft.Extensions.Logging;
using System;

namespace Bit0.Registry.Core.Exceptions
{
    public class PackageVersionNotFoundException : Exception
    {
        public EventId EventId => new EventId(3004, "PackageVersionNotFound");

        public PackageVersionNotFoundException(Package package, Exception innerException) : base(package.ToString(), innerException)
        {
        }
    }
}
