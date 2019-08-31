using Microsoft.Extensions.Logging;
using System;

namespace Bit0.Registry.Core.Exceptions
{
    public class PackageNotFoundException : Exception
    {
        public EventId EventId => new EventId(3003, "PackageNotFound");

        public PackageNotFoundException(String name, String semVer) : this(name, semVer, null)
        {
        }

        public PackageNotFoundException(String name, String semVer, Exception innerException) : base($"Package not found: {name}@{semVer}", innerException)
        {
        }
    }
}
