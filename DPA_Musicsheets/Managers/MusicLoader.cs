
using DPA_Musicsheets.Helpers;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.ViewModels;
using PSAMControlLibrary;
using PSAMWPFControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Managers
{
    /// <summary>
    /// This is the one and only god class in the application.
    /// It knows all about all file types, knows every viewmodel and contains all logic.
    /// TODO: Clean this class up.
    /// </summary>
    public class MusicLoader
    {
        #region Properties
        public string LilypondText { get; set; }
        public List<MusicalSymbol> WPFStaffs { get; set; } = new List<MusicalSymbol>();
        public static List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };

        public Sequence MidiSequence { get; set; }
        #endregion Properties

        public static int _beatNote = 4;    // De waarde van een beatnote.
        public static int _bpm = 120;       // Aantal beatnotes per minute.
        public static int _beatsPerBar;     // Aantal beatnotes per maat.

        public MainViewModel MainViewModel { get; set; }
        public LilypondViewModel LilypondViewModel { get; set; }
        public MidiPlayerViewModel MidiPlayerViewModel { get; set; }
        public StaffsViewModel StaffsViewModel { get; set; }

        #region Midi loading (loads midi to lilypond)
        #endregion Midiloading (loads midi to lilypond)

        #region Staffs loading (loads lilypond to WPF staffs)
        #endregion Staffs loading (loads lilypond to WPF staffs)

        #region Saving to files
        #endregion Saving to files
    }
}
