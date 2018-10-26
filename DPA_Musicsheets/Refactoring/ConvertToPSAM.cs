using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Tokens;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring
{
    class ConvertToPSAM
    {
        public void getStaffsFromTokens(List<IToken> test)
        {
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            Clef currentClef = null;
            int previousOctave = 4;
            char previousNote = 'c';
            bool inRepeat = false;
            bool inAlternative = false;
            int alternativeRepeatNumber = 0;

            for (int i = 0; i < test.Count; i++)
            {
                if (test[i] is INote)
                {
                    INote note = (INote)test[i];
                    int noteLength;//= Int32.Parse(Regex.Match(note.getLength(), @"\d+").Value);
                }
                else if (test[i] is MetaToken)
                {

                }
            }

        }
    }
}
