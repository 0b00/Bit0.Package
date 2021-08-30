namespace Bit0.Registry.Core
{
    /// <summary>
    /// Pack meta data
    /// </summary>
    public interface IPack
    {
        /// <summary>
        /// Pack Id. Use Guid or another unique string
        /// </summary>
        String Id { get; set; }

        /// <summary>
        /// <see cref="FileInfo"/> for the Pack meta data
        /// </summary>
        FileInfo PackFile { get; set; }

        /// <summary>
        /// Pack author
        /// </summary>
        Author Author { get; set; }

        /// <summary>
        /// Pack description
        /// </summary>
        String Description { get; set; }

        /// <summary>
        /// List of pack features
        /// </summary>
        IEnumerable<String> Features { get; set; }

        /// <summary>
        /// Pack Homepage
        /// </summary>
        String Homepage { get; set; }

        /// <summary>
        /// Pack License
        /// </summary>
        License License { get; set; }

        /// <summary>
        /// Pack Name
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// List of pack tags
        /// </summary>
        IEnumerable<String> Tags { get; set; }

        /// <summary>
        /// Pack Version
        /// </summary>
        String Version { get; set; }

        /// <summary>
        /// List of pack dependencies
        /// </summary>
        IEnumerable<String> Dependencies { get; set; }
    }
}