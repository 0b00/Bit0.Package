using Bit0.Package.Core;
using Bit0.Package.Core.Exceptions;
using Bit0.Package.Core.Extensions;
using Bit0.Package.Core.Helpers;
using Divergic.Logging.Xunit;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Xunit.Abstractions;

namespace PackageTests
{
    [ExcludeFromCodeCoverage]
    public class PackageManagerTests
    {
        private readonly ICacheLogger<IPackageManager> _logger;
        private readonly DirectoryInfo _cacheDir;
        private readonly HttpClient _webClient;

        public PackageManagerTests(ITestOutputHelper output)
        {
            _logger = output.BuildLoggerFor<IPackageManager>();

            _cacheDir = new DirectoryInfo(".packs");
            if (_cacheDir.Exists)
            {
                _cacheDir.Delete(true);
                _cacheDir.Create();
            }

            _webClient = new HttpClient();
        }

        [Fact]
        public void Bad_URL()
        {
            var manager = new PackageManager(_logger);
            var action = () => manager.GetPackAsync("https://jain.se").Result;
            action.Should().Throw<InvalidPackFileException>();

            _logger.Last.Should().NotBeNull();
            _logger.Last?.EventId.Id.Should().Be(LogEvents.InvalidPackFile.Id);
        }

        [Theory]
        [InlineData("crunch-log/theme-m20")]
        [InlineData("crunch-log")]
        public void Bad_GitHub_Url(String url)
        {
            var manager = new PackageManager(_logger);
            var action = async () => await manager.GetPackAsync(url);
            action.Should().ThrowAsync<InvalidPackageNameFormatException>();

            _logger.Last.Should().NotBeNull();
            _logger.Last?.EventId.Id.Should().Be(LogEvents.InvalidPackName.Id);
        }

        [Theory]
        [InlineData("crunch-log/theme-m20@1.1.16", "m20", "1.1.16")]
        [InlineData("https://github.com/crunch-log/theme-m20/releases/download/v1.1.16/theme-m20.zip", "m20", "1.1.16")]
        public void Package_Sources(String url, String name, String version)
        {
            var manager = new PackageManager(_logger, _cacheDir, _webClient);
            var pack = manager.GetPackAsync(url).Result;

            pack.Name.Should().Be(name);
            pack.Version.Should().Be(version);

            pack.PackFile.Directory.Should().NotBeNull();
            pack.PackFile.Directory?.FullName.Should().Be(_cacheDir.GetPackDir(pack).FullName);

            pack.Description.Should().Be("m20 minimal theme for Crunchlog.");
            pack.Homepage.Should().Be("https://nullbit.se/crunchlog/themes/default");
            pack.Dependencies.Should().BeNull();

            pack.Dependencies = new List<String>();
            pack.Dependencies.Should().BeEmpty();
        }
    }
}