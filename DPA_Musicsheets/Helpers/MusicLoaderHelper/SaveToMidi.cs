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
        public static void SaveToMidiF(string fileName)
        {
            Sequence sequence = GetSequenceFromWPFStaffs.GetSequenceFromWPFStaff(Managers.MusicLoader.WPFStaffs);
            sequence.Save(fileName);
        }
    }
}
