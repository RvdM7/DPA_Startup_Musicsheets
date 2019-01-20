using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Converters.ConvertHelpers.PSAM;
using DPA_Musicsheets.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters
{
    class ConvertToPSAM : IConverter<ISymbol>
    {
        private Dictionary<Type, IToPSAM> strategies = new Dictionary<Type, IToPSAM>();

        public ConvertToPSAM()
        {
            // Add to PSAM converters
            strategies.Add(typeof(Domain.Note), new NoteToPSAM());
            strategies.Add(typeof(Domain.Rest), new RestToPsam());
            strategies.Add(typeof(Meta), new MetaToPSAM());
            strategies.Add(typeof(Bar), new BarToPSAM());
        }

        object IConverter<ISymbol>.convert(List<ISymbol> symbols)
        {
            List<MusicalSymbol> musicalSymbols = new List<MusicalSymbol>();

            foreach (ISymbol symbol in symbols)
            {
                if (strategies.ContainsKey(symbol.GetType()))
                {
                    strategies[symbol.GetType()].convert(symbol, ref musicalSymbols);
                }
            }
            return musicalSymbols;
        }
    }
}
