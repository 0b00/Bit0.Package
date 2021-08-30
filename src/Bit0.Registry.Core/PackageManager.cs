using Bit0.Registry.Core.Exceptions;
using Bit0.Registry.Core.Extensions;
using Bit0.Registry.Core.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Bit0.Registry.Core
{
    /// <summary>
    /// Helps acquire pack files from several sources
    /// </summary>
    public class PackageManager : IPackageManager
    {
        private readonly DirectoryInfo _packageCacheDir;
        private readonly DirectoryInfo _downlaodCacheDir;
        private readonly ILogger<IPackageManager> _logger;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="packageCacheDir">Package cache directory. Default: <c>%userprofile%\.pack\</c></param>
        /// <param name="httpClient">Http Client</param>
        public PackageManager(ILogger<IPackageManager> logger, DirectoryInfo packageCacheDir = null, HttpClient httpClient = null)
        {
            _logger = logger;

            _packageCacheDir = packageCacheDir
                ?? new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".packs"));
            _downlaodCacheDir = new DirectoryInfo(Path.Combine(_packageCacheDir.FullName, ".downloadCache"));
            if (!_downlaodCacheDir.Exists)
            {
                _downlaodCacheDir.Create();
            }

            _httpClient = httpClient ?? new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"Bit0.Registry");
        }

        public async Task<IPack> GetPackAsync(String source)
        {
            return await GetPackAsync(GetPackUri(source));
        }

        public async Task<IPack> GetPackAsync(Uri uri)
        {
            try
            {
                // Download pack if not cached
                var file = uri.GetDownloadFileInfo(_downlaodCacheDir);
                if (!file.Exists)
                {
                    using (Stream contentStream = await (await _httpClient.GetAsync(uri)).Content.ReadAsStreamAsync(),
                                  fileStream = new FileStream(file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                    {
                        await contentStream.CopyToAsync(fileStream);
                    }
                }

                IPack pack;
                using (var zip = ZipFile.Open(file.FullName, ZipArchiveMode.Read, Encoding.UTF8))
                {
                    _logger.LogInformation(LogEvents.DownloadedPack, $"Downloaded Pack archive: {uri}");

                    // Parse pack meta file
                    var packEntry = zip.GetEntry("pack.json");
                    using (var jr = packEntry.OpenJsonReader())
                    {
                        var serializer = new JsonSerializer();
                        pack = serializer.Deserialize<Pack>(jr);
                        _logger.LogInformation(LogEvents.ReadPack, $"Read Pack file: {packEntry.FullName}");
                    }
                    var packDir = _packageCacheDir.GetPackDir(pack);

                    // Extract pack
                    if (!packDir.Exists)
                    {
                        zip.ExtractToDirectory(packDir.FullName);
                        _logger.LogInformation(LogEvents.ExtractedPack, $"Extracted Pack archive to: {packDir.FullName}");
                    }
                    pack.PackFile = packDir.GetPackFile();
                }

                return pack;
            }
            catch (Exception ex)
            {
                var exp = new InvalidPackFileException(uri.ToString(), ex);
                _logger.LogError(exp.EventId, exp, "Invalid Pack file");
                throw exp;
            }
        }

        private Uri GetPackUri(String source)
        {
            if (Uri.TryCreate(source, UriKind.Absolute, out var uri))
            {
                return uri;
            }

            return GetGithubUrl(source);
        }

        private Uri GetGithubUrl(String source)
        {
            var match = new Regex(@"^(?<username>.+)\/(?<repo>.+)\@(?<version>.+)$").Match(source);
            if (!match.Success)
            {
                var exp = new InvalidPackageNameFormatException(
                    $"Invalid GitHub source package format: '{source}'. Should be: username/repo@version");
                _logger.LogError(exp.EventId, exp, exp.Message);
                throw exp;
            }

            var username = match.Groups["username"].Value;
            var repo = match.Groups["repo"].Value;
            var version = match.Groups["version"].Value;

            return new Uri($"https://github.com/{username}/{repo}/releases/download/v{version}/{repo}.zip", UriKind.Absolute);
        }
    }
}
