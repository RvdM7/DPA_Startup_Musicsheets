using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Models.MusicNotes
{
    class Unknown : IMusicSymbol
    {
        public string value => throw new NotImplementedException();
    }
}
