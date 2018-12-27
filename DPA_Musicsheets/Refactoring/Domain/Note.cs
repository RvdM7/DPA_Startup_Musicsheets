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
        private Dots _dots;
        public Dots dots
        {
            get
            {
                if (_dots == null)
                {
                    _dots = new Dots();
                }
                return _dots;
            }
            set
            {
                _dots = value;
            }
        }
        public IOctaveModifier octaveModifier { get; set; }
        public int octave = 4;
        public int duration = 0;
        public NoteHeight height { get; private set; }


        public Note(NoteHeight noteHeight)
        {
            height = noteHeight;
            dots = null;
            octaveModifier = null;
        }

        public override string ToString()
        {
            return $"{height}{duration}";
        }
    }
}
