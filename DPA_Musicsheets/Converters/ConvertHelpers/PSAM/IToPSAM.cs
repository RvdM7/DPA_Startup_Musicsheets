using DPA_Musicsheets.Domain;
using PSAMControlLibrary;
using System.Collections.Generic;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
{
    interface IToPSAM
    {
        void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols);
    }
}
