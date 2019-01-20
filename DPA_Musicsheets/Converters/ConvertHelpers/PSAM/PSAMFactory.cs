using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
{
    static class PSAMFactory
    {
        public static Barline getBarline()
        {
            return new Barline();
        }

        public static Clef getClef(ClefType clefType, int whichLine)
        {
            return new Clef(clefType, whichLine);
        }

        public static TimeSignature getTimeSignature(TimeSignatureType timeSignatureType, uint beats, uint beatType)
        {
            return new TimeSignature(timeSignatureType, beats, beatType);
        }

        public static Note getNote(string noteStep, int noteAlter, int noteOctave, MusicalSymbolDuration noteDuration, NoteStemDirection noteStemDirection, NoteTieType noteTieType, List<NoteBeamType> noteBeamList)
        {
            return new Note(noteStep, noteAlter, noteOctave, noteDuration, noteStemDirection, noteTieType, noteBeamList);
        }

        public static Rest getRest(MusicalSymbolDuration restDuration)
        {
            return new Rest(restDuration);
        }
    }
}
