using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.PSAM
{
    class RestToPsam : IToPSAM
    {
        public void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols)
        {
            var rest = symbol as Domain.Rest;

            musicalSymbols.Add(new PSAMControlLibrary.Rest((MusicalSymbolDuration)rest.duration));
        }
    }
}
