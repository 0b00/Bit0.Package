namespace Bit0.Package.Core.Extensions
{
    public static class PathExtentions
    {
        public static DirectoryInfo GetPackDir(this DirectoryInfo packageCacheDir, IPack pack)
        {
            return packageCacheDir.GetPackDir(pack.Id, pack.Version);
        }

        public static DirectoryInfo GetPackDir(this DirectoryInfo packageCacheDir, String name, String semVer)
        {
            return new DirectoryInfo(Path.Combine(packageCacheDir.FullName, name, semVer));
        }

        public static FileInfo GetDownloadFileInfo(this Uri uri, DirectoryInfo baseDir)
        {
            return new FileInfo(Path.Combine(baseDir.FullName, uri.AbsoluteUri.Replace('_', ':', '/')));
        }

        public static FileInfo GetPackFile(this DirectoryInfo packDir)
        {
            return packDir.GetFiles("pack.json", SearchOption.TopDirectoryOnly).Single();
        }
    }
}
