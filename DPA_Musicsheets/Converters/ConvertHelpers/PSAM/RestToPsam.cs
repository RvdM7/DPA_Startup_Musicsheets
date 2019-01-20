using System.Collections.Generic;
using DPA_Musicsheets.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
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
