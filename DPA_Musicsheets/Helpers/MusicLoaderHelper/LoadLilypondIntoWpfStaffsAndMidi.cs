using DPA_Musicsheets.Models;
using DPA_Musicsheets.ViewModels;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class LoadLilypondIntoWpfStaffsAndMidi
    {
        /// <summary>
        /// This creates WPF staffs and MIDI from Lilypond.
        /// TODO: Remove the dependencies from one language to another. If we want to replace the WPF library with another for example, we have to rewrite all logic.
        /// TODO: Create our own domain classes to be independent of external libraries/languages.
        /// </summary>
        /// <param name="content"></param>
        static public void LoadLilypondIntoWpfStaffsAndMidiF(string content)
        {
            Sequence MidiSequence = Managers.MusicLoader.MidiSequence;
            List<MusicalSymbol> WPFStaffs = Managers.MusicLoader.WPFStaffs;
            MidiPlayerViewModel MidiPlayerViewModel = Managers.MusicLoader.MidiPlayerViewModel;
            StaffsViewModel StaffsViewModel = Managers.MusicLoader.StaffsViewModel;


            string LilypondText = content; // Note: was eerst een globale variabele
            content = content.Trim().ToLower().Replace("\r\n", " ").Replace("\n", " ").Replace("  ", " ");
            LinkedList<LilypondToken> tokens = GetTokensFromLilypond.GetTokensFromLilypondF(content);
            WPFStaffs.Clear();

            WPFStaffs.AddRange(GetStaffsFromTokens.GetStaffsFromTokensF(tokens));
            StaffsViewModel.SetStaffs(WPFStaffs);

            MidiSequence = GetSequenceFromWPFStaffs.GetSequenceFromWPFStaff(WPFStaffs);
            MidiPlayerViewModel.MidiSequence = MidiSequence;
        }
    }
}
