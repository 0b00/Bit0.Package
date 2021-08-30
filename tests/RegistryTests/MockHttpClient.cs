using System.Diagnostics.CodeAnalysis;

namespace RegistryTests
{
    [ExcludeFromCodeCoverage]
    public class MockHttpClient : HttpClient
    {
        //protected override WebRequest GetWebRequest(Uri address)
        //{
        //    var url = address.AbsoluteUri.Replace("http://feed1.test/", "TestData/registry1/");
        //    address = new Uri(new FileInfo(url).FullName.NormalizePath());
        //    return base.GetWebRequest(address);
        //}
    }
}
