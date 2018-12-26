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
        public Dots dots = new Dots();
        public int duration = 0;
        public NoteHeight height;

        public override string ToString()
        {
            return "Note: " + duration + height + dots.dots;
        }
    }
}
