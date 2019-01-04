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

            printList(music);

            MusicLoadedEventArgs args = new MusicLoadedEventArgs
            {
                staffsConverter = new ConvertToPSAM(),
                symbolList = music
            };
            onMusicLoaded(args);
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
