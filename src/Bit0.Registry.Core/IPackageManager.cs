namespace Bit0.Registry.Core
{
    public interface IPackageManager
    {
        /// <summary>
        /// Gets a pack
        /// </summary>
        /// <param name="source">
        /// Pack Source
        /// <code>
        /// Valid Package Source Examples:
        /// https://example.com/path/package_version.zip
        /// file://C:/packages/package/package_version.zip
        /// username/repo@version
        /// </code>
        /// </param>
        /// <returns></returns>
        public Task<IPack> GetPackAsync(String source);

        /// <summary>
        /// Gets a Pack from a Uri
        /// </summary>
        /// <param name="uri">Pack Source</param>
        /// <returns></returns>
        public Task<IPack> GetPackAsync(Uri uri);
    }
}