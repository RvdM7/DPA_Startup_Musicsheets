using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PSAMControlLibrary;
using DPA_Musicsheets.Refactoring.Tokens;

namespace DPA_Musicsheets.Refactoring
{
    class ConvertToPSAM
    {

        public List<MusicalSymbol> getStaffsFromTokens(List<IToken> test)
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
                IToken token = test[i];
                if (token is INote)
                {
                    INote note = (INote)test[i];
                    var PSAMNote = new PSAMControlLibrary.Note(note.getHeight().ToString(), 0, previousOctave, (MusicalSymbolDuration)note.getLength(), NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    //PSAMNote.NumberOfDots += dots;
                    symbols.Add(PSAMNote);
                }
                else if (token is BarToken)
                {
                    symbols.Add(new PSAMControlLibrary.Barline());
                }
                else if (token is MetaToken)
                {
                }
            }
            return symbols;
        }
    }
}
