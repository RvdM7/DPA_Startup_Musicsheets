using System.Collections.Generic;
using DPA_Musicsheets.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
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

            var PSAMNote = PSAMFactory.getNote(note.height.ToString().ToUpper(), alter, note.Octave, (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
            PSAMNote.NumberOfDots += note.dots != null ? note.dots.dots : 0;

            musicalSymbols.Add(PSAMNote);
        }
    }
}
