using DPA_Musicsheets.Refactoring.Load;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using DPA_Musicsheets.Refactoring.Converters;

namespace DPA_Musicsheets.Refactoring
{
    public class MusicLoader
    {
        public event EventHandler<MusicLoadedEventArgs> musicLoaded;

        public void loadMusic(String fileName)
        {
            ILoader loader = new LoadLocator().LocateLoader(fileName);
            List<ISymbol> music = loader.loadMusic();

            music.RemoveAll(remove);

            printList(music);

            MusicLoadedEventArgs args = new MusicLoadedEventArgs
            {
                editorConverter = new ConvertToLilypond(),
                staffsConverter = new ConvertToPSAM(),
                symbolList = music
            };
            onMusicLoaded(args);
        }

        private static bool remove(ISymbol symbol)
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

        private void cleanUpList(ref List<ISymbol> symbols)
        {
            symbols.RemoveAll(item => item == null);
            //symbols.RemoveAll(item => )
        }

        private void printList(List<ISymbol> list)
        {
            foreach (ISymbol symbol in list)
            {
                System.Diagnostics.Debug.Write(symbol + " ");
            }
            System.Diagnostics.Debug.WriteLine("");
        }

        protected virtual void onMusicLoaded(MusicLoadedEventArgs e)
        {
            musicLoaded?.Invoke(this, e);
        }
    }

    public class MusicLoadedEventArgs : EventArgs
    {
        public List<ISymbol> symbolList;
        public IConverter<ISymbol> staffsConverter;
        public IConverter<ISymbol> editorConverter;
    }


}
