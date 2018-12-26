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

            //System.Diagnostics.Debug.WriteLine(music[0]);
            //System.Diagnostics.Debug.WriteLine(music[2]);

            MusicLoadedEventArgs args = new MusicLoadedEventArgs();
            args.converter = new ConvertToPSAM();
            args.symbolList = music;
            onMusicLoaded(args);
        }

        protected virtual void onMusicLoaded(MusicLoadedEventArgs e)
        {
            musicLoaded?.Invoke(this, e);
        }

        /*
        private void showMusic()
        {
            ConvertToPSAM convertToPSAM = new ConvertToPSAM();
            staffsViewModel.SetStaffs(convertToPSAM.getStaffsFromTokens(music));

            lilypondViewModel.LilypondTextLoaded("lil tekst");

        }*/
    }

    public class MusicLoadedEventArgs : EventArgs
    {
        public List<ISymbol> symbolList { get; set; }
        public IConverter<ISymbol> converter { get; set; }
    }
}
