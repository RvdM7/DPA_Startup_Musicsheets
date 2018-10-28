using DPA_Musicsheets.Refactoring.Load;
using DPA_Musicsheets.Refactoring.Tokens;
using DPA_Musicsheets.ViewModels; // TODO remove
using System;
using System.Collections.Generic;
using System.Text;

namespace DPA_Musicsheets.Refactoring
{
    public class MusicLoader
    {
        List<IToken> music;
        public static MainViewModel MainViewModel { get; set; }
        public static LilypondViewModel LilypondViewModel { get; set; }
        public static MidiPlayerViewModel MidiPlayerViewModel { get; set; }
        public static StaffsViewModel StaffsViewModel { get; set; }

        public void loadMusic(String fileName)
        {
            LoadLocator ll = new LoadLocator();
            ILoader fl = ll.LocateLoader(fileName);
            music = fl.loadMusic();
            showMusic();
        }

        public void showMusic()
        {
            ConvertToPSAM ctp = new ConvertToPSAM();
            ctp.getStaffsFromTokens(music);
            StaffsViewModel.SetStaffs(ctp.getStaffsFromTokens(music));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < music.Count; i++)
            {
                try
                {
                    if (music[i] is INote)
                    {
                        INote note = (INote)music[i];
                        sb.Append(note.getHeight().ToString());
                        sb.AppendLine(note.getLength().ToString());
                        sb.AppendLine(note.ToString());
                    }
                    else if (music[i] is MetaToken)
                    {
                        MetaToken mt = (MetaToken)music[i];
                        sb.Append("BPM: ");
                        sb.AppendLine(mt.bpm.ToString());
                        sb.Append("beatNote: ");
                        sb.AppendLine(mt.beatNote.ToString());
                        sb.Append("beatsPerBar: ");
                        sb.AppendLine(mt.beatsPerBar.ToString());
                    }

                    sb.AppendLine();
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            LilypondViewModel.LilypondTextLoaded(sb.ToString());
        }
    }
}
