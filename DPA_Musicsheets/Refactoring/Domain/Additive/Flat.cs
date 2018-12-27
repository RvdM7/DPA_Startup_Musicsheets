using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain.Additive
{
    class Flat : IOctaveModifier
    {
        private int flats = 1;

        public void addModifier(int additive)
        {
            //throw new NotImplementedException();
            flats += additive;
        }

        public int getModifier()
        {
            //throw new NotImplementedException();
            return flats;
        }
    }
}
