using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class SaveToMidi
    {
        internal void SaveToMidiF(string fileName)
        {
            Sequence sequence = GetSequenceFromWPFStaffs.GetSequenceFromWPFStaff(WPFStaffs, _beatNote, _beatsPerBar, _bpm);


            sequence.Save(fileName);
        }
    }
}
