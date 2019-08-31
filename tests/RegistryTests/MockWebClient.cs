using Bit0.Registry.Core.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

namespace RegistryTests
{
    [ExcludeFromCodeCoverage]
    public class MockWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var url = address.AbsoluteUri.Replace("http://feed1.test/", "TestData/registry1/");
            address = new Uri(new FileInfo(url).FullName.NormalizePath());
            return base.GetWebRequest(address);
        }
    }
}
