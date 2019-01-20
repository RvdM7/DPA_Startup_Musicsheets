using System;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Lilypond
{
    class BarToLilypond : IToLilypondConverter
    {
        public string convert(ISymbol symbol)
        {
            return "|" + Environment.NewLine;
        }
    }
}
