namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Documents;

    using ModernControls;

    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Initializes static members of the <see cref="App"/> class.
        /// </summary>
        static App()
        {
            var library = Assembly.GetEntryAssembly().GetManifestResourceNames();

            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
                {
                    var dll = e.Name.Contains(",")
                                  ? e.Name.Substring(0, e.Name.IndexOf(','))
                                  : e.Name.Replace(".dll", string.Empty).Replace(".", "_");

                    var item = library.FirstOrDefault(x => x.Contains(dll));

                    if (item != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(item);
                            if (stream != null)
                            {
                                stream.CopyTo(ms);
                            }

                            try
                            {
                                return Assembly.Load(ms.ToArray());
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }

                    return null;
                };
        }
    }
}
