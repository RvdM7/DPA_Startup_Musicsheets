using System.Collections.Generic;
using DPA_Musicsheets.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
{
    class BarToPSAM : IToPSAM
    {
        public void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols)
        {
            musicalSymbols.Add(PSAMFactory.getBarline());
        }
    }
}
