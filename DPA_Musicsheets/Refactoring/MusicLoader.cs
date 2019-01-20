using DPA_Musicsheets.Refactoring.Load;
using System;

namespace DPA_Musicsheets.Refactoring
{
    public class MusicLoader
    {
        MusicList musicList;

        public MusicLoader(MusicList musicList)
        {
            this.musicList = musicList;
        }

        public void loadFromFile(String fileName)
        {
            musicList.Music = new LoadLocator().LocateLoader(fileName).loadFromFile();
        }

        public void loadFromEditor(string editorloads)
        {
            musicList.Music = new LoadLilypond().loadFromString(editorloads);
        }
    }
}
