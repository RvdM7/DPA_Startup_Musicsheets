using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Domain.Clefs
{
    static class ClefFactory
    {
        public static Treble getTreble()
        {
            return Treble.create();
        }

        public static Bass getBass()
        {
            return Bass.create();
        }
    }
}
