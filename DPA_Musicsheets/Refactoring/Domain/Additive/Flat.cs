using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain.Additive
{
    class Flat : ICrossMole
    {
        private readonly int flats;

        public Flat() { flats = 1; }

        public Flat(int flats)
        {
            this.flats = flats;
        }

        public int getModifier()
        {
            //throw new NotImplementedException();
            return flats;
        }
    }
}
