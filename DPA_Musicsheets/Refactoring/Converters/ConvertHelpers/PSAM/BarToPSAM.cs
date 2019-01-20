using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.PSAM
{
    class BarToPSAM : IToPSAM
    {
        public void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols)
        {
            musicalSymbols.Add(new Barline());
        }
    }
}
