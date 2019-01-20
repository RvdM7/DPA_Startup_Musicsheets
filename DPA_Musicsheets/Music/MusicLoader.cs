using DPA_Musicsheets.Load;
using System;

namespace DPA_Musicsheets.Music
{
    public class MusicLoader
    {
        MusicList musicList;

        public MusicLoader(MusicList musicList)
        {
            this.musicList = musicList;
        }

        public void loadFromFile(string fileName)
        {
            musicList.Music = new LoadLocator().LocateLoader(fileName).loadFromFile();
        }

        public void loadFromEditor(string editorloads)
        {
            musicList.Music = new LoadLilypond().loadFromString(editorloads);
        }
    }
}
