using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.PSAM
{
    interface IToPSAM
    {
        void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols);
    }
}
