using System;
using System.Collections.Generic;
using System.IO;

namespace Bit0.Registry.Core
{
    public interface IPack
    {
        String Id { get; set; }
        FileInfo PackFile { get; set; }
        Author Author { get; set; }
        String Description { get; set; }
        IEnumerable<String> Features { get; set; }
        String Homepage { get; set; }
        License License { get; set; }
        String Name { get; set; }
        IEnumerable<String> Tags { get; set; }
        String Version { get; set; }
        IDictionary<String, String> Dependencies { get; set; }
    }
}