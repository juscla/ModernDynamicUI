namespace WpfApp1
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The assembly loader.
    /// </summary>
    public static class AssemblyLoader
    {
        /// <summary>
        /// The entry assembly.
        /// </summary>
        private static readonly Assembly Entry = Assembly.GetEntryAssembly();

        /// <summary>
        /// The names.
        /// </summary>
        private static readonly string[] Names = Entry.GetManifestResourceNames();

        /// <summary>
        /// The load.
        /// </summary>
        /// <param name="temporaryDirectory">
        /// The temporary directory.
        /// </param>
        public static void Load(string temporaryDirectory)
        {
            // dump all the assemblies
            DumpAllAssemblies(temporaryDirectory);

            // read all assemblies in the temp directory. 
            var library = Directory.GetFiles(temporaryDirectory);

            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
                {
                    // split the name on a comma.
                    var entry = e.Name.Split(',');

                    // try to read the name from our library
                    var name = entry.Length > 0 ? library.FirstOrDefault(x => x.Contains(entry[0])) : null;

                    return !string.IsNullOrEmpty(name) ? Assembly.LoadFile(name) : null;
                };
        }

        /// <summary>
        /// The dump all assemblies.
        /// </summary>
        /// <param name="temporaryDirectory">
        /// The temporary directory.
        /// </param>
        private static void DumpAllAssemblies(string temporaryDirectory)
        {
            // create our library storage location
            // if it doesn't already exist. 
            Directory.CreateDirectory(temporaryDirectory);

            // write all the embedded Libraries to the Temporary Location. 
            // we will always overwrite them. 
            foreach (var entry in Names.Select(x => new AssemblyLoadInfo(x, Entry.GetManifestResourceStream(x)))
                .Where(x => x.IsValid))
            {
                using (var f = File.OpenWrite(temporaryDirectory + entry.Name))
                {
                    entry.Stream.CopyTo(f);
                }
            }
        }

        /// <summary>
        /// The assembly load info.
        /// </summary>
        private struct AssemblyLoadInfo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AssemblyLoadInfo"/> struct.
            /// </summary>
            /// <param name="name">
            /// The name.
            /// </param>
            /// <param name="stream">
            /// The stream.
            /// </param>
            public AssemblyLoadInfo(string name, Stream stream)
            {
                this.Name = name;
                this.Stream = stream;
            }

            /// <summary>
            /// Gets the name.
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// Gets the stream.
            /// </summary>
            public Stream Stream { get; private set; }

            /// <summary>
            /// Gets a value indicating whether is valid.
            /// </summary>
            public bool IsValid => this.Stream != null;
        }
    }
}
