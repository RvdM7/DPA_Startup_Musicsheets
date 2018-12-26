using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring
{
    class ConvertToPSAM : IConverter<ISymbol>
    {
        object IConverter<ISymbol>.convert(List<ISymbol> test)
        {
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            Clef currentClef = null;
            int previousOctave = 4;
            char previousNote = 'c';
            bool inRepeat = false;
            bool inAlternative = false;
            int alternativeRepeatNumber = 0;
            List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };

            symbols.Add(new PSAMControlLibrary.Clef(ClefType.GClef, 2));
            for (int i = 0; i < test.Count; i++)
            {
                ISymbol symbol = test[i];

                if (symbol is Domain.Note)
                {
                    Domain.Note note = (Domain.Note)test[i];
                    var PSAMNote = new PSAMControlLibrary.Note(note.height.ToString(), 0, previousOctave, (MusicalSymbolDuration)note.duration, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    PSAMNote.NumberOfDots += note.dots.dots;
                    symbols.Add(PSAMNote);
                }
                else if (symbol is Meta)
                {
                }
                else if (symbol is Bar)
                {
                    symbols.Add(new Barline() { AlternateRepeatGroup = 0 });
                }
            }
            return symbols;
        }
    }
}
