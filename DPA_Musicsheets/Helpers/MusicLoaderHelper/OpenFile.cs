using DPA_Musicsheets.ViewModels;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class OpenFile
    {
        /// <summary>
        /// Opens a file.
        /// TODO: Remove the switch cases and delegate.
        /// TODO: Remove the knowledge of filetypes. What if we want to support MusicXML later?
        /// TODO: Remove the calling of the outer viewmodel layer. We want to be able reuse this in an ASP.NET Core application for example.
        /// </summary>
        /// <param name="fileName"></param>
        static public void OpenRequestedFile(string fileName)
        {
            MidiPlayerViewModel MidiPlayerViewModel = Managers.MusicLoader.MidiPlayerViewModel;
            Sequence MidiSequence = Managers.MusicLoader.MidiSequence;
            LilypondViewModel LilypondViewModel = Managers.MusicLoader.LilypondViewModel;
            string LilypondText = Managers.MusicLoader.LilypondText;
            if (Path.GetExtension(fileName).EndsWith(".mid"))
            {
                MidiSequence = new Sequence();
                MidiSequence.Load(fileName);

                MidiPlayerViewModel.MidiSequence = MidiSequence;
                LilypondText = LoadMidiIntoLilypond.LoadMidiIntoLilypondF(MidiSequence);
                LilypondViewModel.LilypondTextLoaded(LilypondText);
            }
            else if (Path.GetExtension(fileName).EndsWith(".ly"))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var line in File.ReadAllLines(fileName))
                {
                    sb.AppendLine(line);
                }

                LilypondText = sb.ToString();
                LilypondViewModel.LilypondTextLoaded(LilypondText);
            }
            else
            {
                throw new NotSupportedException($"File extension {Path.GetExtension(fileName)} is not supported.");
            }

            LoadLilypondIntoWpfStaffsAndMidi.LoadLilypondIntoWpfStaffsAndMidiF(LilypondText);
        }
    }
}
