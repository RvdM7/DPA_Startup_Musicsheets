using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.PSAM
{
    class NoteToPSAM : IToPSAM
    {
        public void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols)
        {
            var note = symbol as Domain.Note;

            // Length
            int noteLength = note.duration;

            // Crosses and Moles
            int alter = note.crossMole != null ? note.crossMole.getModifier() : 0;

            var PSAMNote = new PSAMControlLibrary.Note(note.height.ToString().ToUpper(), alter, note.Octave, (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
            PSAMNote.NumberOfDots += note.dots != null ? note.dots.dots : 0;

            musicalSymbols.Add(PSAMNote);
        }
    }
}
