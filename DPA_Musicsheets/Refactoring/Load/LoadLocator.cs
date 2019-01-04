using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLocator
    {
        private Dictionary<string, ILoader> loaders = new Dictionary<string, ILoader>();

        public LoadLocator()
        {
            // Add loaders
            loaders.Add(".mid", new LoadMidi());
            loaders.Add(".ly", new LoadLilypond());
        }

        public ILoader LocateLoader(string fileName)
        {
            try
            {
                ILoader loader = loaders[Path.GetExtension(fileName)];
                loader.fileName = fileName;
                return loader;
            }
            catch (Exception)
            {
                throw new NotSupportedException($"File extension {Path.GetExtension(fileName)} is not supproted.");
            }
        }
    }
}
