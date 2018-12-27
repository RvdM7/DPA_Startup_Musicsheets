using DPA_Musicsheets.Refactoring.Load;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA_Musicsheets.Refactoring
{
    public class MusicLoader
    {
        public event EventHandler<MusicLoadedEventArgs> musicLoaded;

        public void loadMusic(String fileName)
        {
            ILoader loader = new LoadLocator().LocateLoader(fileName);
            List<ISymbol> music = loader.loadMusic();

            foreach (ISymbol symbol in music)
            {
                System.Diagnostics.Debug.Write(symbol + " ");
            }

            MusicLoadedEventArgs args = new MusicLoadedEventArgs();
            args.converter = new ConvertToPSAM();
            args.symbolList = music;
            onMusicLoaded(args);
        }

        protected virtual void onMusicLoaded(MusicLoadedEventArgs e)
        {
            musicLoaded?.Invoke(this, e);
        }
    }

    public class MusicLoadedEventArgs : EventArgs
    {
        public List<ISymbol> symbolList { get; set; }
        public IConverter<ISymbol> converter { get; set; }
    }
}
