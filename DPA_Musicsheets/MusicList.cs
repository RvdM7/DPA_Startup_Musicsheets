using System;
using System.Collections.Generic;
using DPA_Musicsheets.Events;
using DPA_Musicsheets.Domain;
using DPA_Musicsheets.Converters;

namespace DPA_Musicsheets
{
    public class MusicList
    {
        public event EventHandler<MusicLoadedEventArgs> musicLoaded;
        private MusicLoadedEventArgs args = new MusicLoadedEventArgs
        {
            editorConverter = new ConvertToLilypond(),
            staffsConverter = new ConvertToPSAM()
        };

        private List<ISymbol> music;
        public List<ISymbol> Music
        {
            get
            {
                return music;
            }
            set
            {
                value.RemoveAll(unnecessary);
                music = value;
                args.symbolList = music;
                onMusicLoaded(args);
            }
        }

        protected void onMusicLoaded(MusicLoadedEventArgs e)
        {
            musicLoaded?.Invoke(this, e);
        }

        private static bool unnecessary(ISymbol symbol)
        {
            if (symbol == null)
            {
                return true;
            }
            if (symbol is Meta meta && !meta.isReady())
            {
                return true;
            }
            return false;
        }

        private static void printList(List<ISymbol> list)
        {
            foreach (ISymbol symbol in list)
            {
                System.Diagnostics.Debug.Write(symbol + " ");
            }
            System.Diagnostics.Debug.WriteLine("");
        }
    }
}
