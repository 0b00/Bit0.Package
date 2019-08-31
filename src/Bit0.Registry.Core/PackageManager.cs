using Bit0.Registry.Core.Exceptions;
using Bit0.Registry.Core.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SemVer;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using Version = SemVer.Version;

namespace Bit0.Registry.Core
{
    public class PackageManager : IPackageManager
    {
        private readonly DirectoryInfo _packageCacheDir;
        private readonly ILogger<IPackageManager> _logger;
        private readonly WebClient _webClient;

        public PackageManager(DirectoryInfo packageCacheDir, WebClient webClient, ILogger<IPackageManager> logger)
        {
            _packageCacheDir = packageCacheDir;
            _logger = logger;
            _webClient = webClient;
        }

        public void AddFeed(String name, String url)
        {
            _logger.LogInformation(new EventId(3000), $"Add feed: {name} {url}");

            var uri = new Uri(url + "index.json");

            var feed = GetFeed(uri);
            if (feed != null)
            {
                Feeds.Add(name, feed);
            }
        }

        public IDictionary<String, PackageFeed> Feeds { get; } = new Dictionary<String, PackageFeed>();

        public IEnumerable<Package> Packages => Feeds.SelectMany(f => f.Value.Package);

        public IEnumerable<Package> Find(String name)
        {
            _logger.LogInformation(new EventId(3000), $"Find package: {name}");

            var packages = Packages.Where(p =>
                p.Name.ToLowerInvariant().Contains(name.ToLowerInvariant())
                || p.Id.ToLowerInvariant().Contains(name.ToLowerInvariant()));

            _logger.LogInformation(new EventId(3000), $"Found packages: {packages.Count()}");
            return packages;
        }

        public Package GetPackage(String name, String semVer)
        {
            _logger.LogInformation(new EventId(3000), $"Get package: {name} {semVer}");

            var package = Packages.Where(p => p.Id == name).SingleOrDefault();

            if (package == null)
            {
                var exp = new PackageNotFoundException(name, semVer);
                _logger.LogError(exp.EventId, exp, exp.Message);
                throw exp;
            }

            return package;
        }

        public PackageVersion GetPackageVersion(String name, String semVer)
        {
            _logger.LogInformation(new EventId(3000), $"Get packageVersion: {name} {semVer}");

            var package = GetPackage(name, semVer);
            return GetPackageVersion(package, semVer);
        }

        public PackageVersion GetPackageVersion(Package package, String semVer)
        {
            try
            {
                var versions = package.Versions.Select(v => new Version(v.Version));
                var version = new Range(semVer).MaxSatisfying(versions);
                var packageVersion = package.Versions.Where(v => new Version(v.Version) == version).Single();
                return packageVersion;
            }
            catch (Exception ex)
            {
                var exp = new PackageVersionNotFoundException(package, ex);
                _logger.LogError(exp.EventId, exp, "Package version not found");
                throw exp;
            }
        }

        public IPack Get(String name, String semVer)
        {
            return Get(GetPackage(name, semVer), semVer);
        }

        public IPack Get(Package package, String semVer)
        {
            return Get(GetPackageVersion(package, semVer));
        }

        public IPack Get(PackageVersion version)
        {
            return Get(version.Url);
        }

        public IPack Get(String url)
        {
            try
            {
                var file = new FileInfo($"{Path.GetTempFileName()}.pack.zip");
                _webClient.DownloadFile(url, file.FullName);

                ZipArchive zip;
                zip = ZipFile.Open(file.FullName, ZipArchiveMode.Read, Encoding.UTF8);
                _logger.LogInformation(new EventId(3000), $"Downloaded Pack archive: {url}");

                IPack pack;
                var packEntry = zip.GetEntry("pack.json");
                using (var jr = packEntry.OpenJsonReader())
                {
                    var serializer = new JsonSerializer();
                    pack = serializer.Deserialize<Pack>(jr);
                    _logger.LogInformation(new EventId(3000), $"Read Pack file: {packEntry.FullName}");
                }

                // TODO: Use CombinePath Extensions
                var packDir = new DirectoryInfo($"{_packageCacheDir.FullName}/{pack.Id}/{pack.Version}");

                if (!packDir.Exists)
                {
                    zip.ExtractToDirectory(packDir.FullName);
                    _logger.LogInformation(new EventId(3000), $"Extracted Pack archive to: {packDir.FullName}");
                }

                pack.PackFile = new List<FileInfo>(packDir.GetFiles("pack.json", SearchOption.TopDirectoryOnly)).Single();

                return pack;
            }
            catch (Exception ex)
            {
                var exp = new InvalidPackFileException(url, ex);
                _logger.LogError(exp.EventId, exp, "Invalid Pack file");
                throw exp;
            }
        }


        public IEnumerable<IPack> GetDependancyPacks(Package package)
        {
            var deps = GetDependancies(package);
            var lst = deps.Select(dep => Get(dep.Key, dep.Value));

            return lst;
        }

        public IDictionary<String, String> GetDependancies(Package package)
        {
            var deps = new Dictionary<String, String>();
            GetDependancies(package, ref deps);
            return deps;
        }

        private void GetDependancies(Package package, ref Dictionary<String, String> dependancies)
        {
            if (package == null || package.Dependencies == null)
            {
                return;
            }

            foreach (var dep in package.Dependencies)
            {
                if (!dependancies.ContainsKey(dep.Key))
                {
                    dependancies.Add(dep.Key, dep.Value);
                }
                var dPkg = Packages.First(p => p.Id == dep.Key);
                GetDependancies(dPkg, ref dependancies);
            }
        }

        private PackageFeed GetFeed(Uri url)
        {
            try
            {
                using (var file = _webClient.OpenRead(url).GetJsonReader())
                {
                    _logger.LogInformation(new EventId(3000), $"Found package feed: {url}");
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<PackageFeed>(file);
                }
            }
            catch (Exception ex)
            {
                var exp = new InvalidFeedException(url, ex);
                _logger.LogInformation(exp.EventId, exp, $"Could load package feed: {url}");
                throw exp;
            }
        }

    }
}
