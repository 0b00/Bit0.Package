using System;
using System.Collections.Generic;

namespace Bit0.Registry.Core
{
    public interface IPackageManager
    {
        void AddFeed(String name, String url);
        IEnumerable<Package> Find(String name);
        Package GetPackage(String name, String semVer);
        PackageVersion GetPackageVersion(String name, String semVer);
        PackageVersion GetPackageVersion(Package package, String semVer);
        IPack GetPack(String name);
        IPack GetPack(String name, String semVer = "");
        IPack GetPack(Package package, String semVer);
        IPack GetPack(PackageVersion packageVersion);
        IPack GetPack(Uri uri);
        IEnumerable<IPack> GetDependancyPacks(Package package);
        IDictionary<String, String> GetDependancies(Package package);
        IEnumerable<Package> Packages { get; }
        IDictionary<String, PackageFeed> Feeds { get; }
    }
}
