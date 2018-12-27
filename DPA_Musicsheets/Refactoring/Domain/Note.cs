using DPA_Musicsheets.Refactoring.Domain.Additive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain
{
    class Note : ISymbol
    {
        public Dots dots = null;
        public IOctaveModifier octaveModifier = null;
        public int octave;
        public int duration = 0;
        public readonly NoteHeight height;


        public Note(NoteHeight noteHeight)
        {
            height = noteHeight;
        }

        public override string ToString()
        {
            return $"{height}{duration}";
        }
    }
}
