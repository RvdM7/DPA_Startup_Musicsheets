using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain.Additive
{
    class Sharp : IOctaveModifier
    {
        private readonly int sharps;

        public Sharp() { sharps = 1; }

        public Sharp(int sharps)
        {
            this.sharps = sharps;
        }

        public int getModifier()
        {
            //throw new NotImplementedException();
            return -sharps;
        }
    }
}
