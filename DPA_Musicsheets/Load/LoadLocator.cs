using System;
using System.Collections.Generic;
using System.IO;

namespace DPA_Musicsheets.Load
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
                loader.file = fileName;
                return loader;
            }
            catch (Exception)
            {
                throw new NotSupportedException($"File extension {Path.GetExtension(fileName)} is not supproted.");
            }
        }
    }
}
