using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain
{
    class Rest : ISymbol
    {
        public int duration;

        public Rest() { }

        public Rest(int duration)
        {
            this.duration = duration;
        }

        public override string ToString()
        {
            return $"'r{duration}'";
        }
    }
}
