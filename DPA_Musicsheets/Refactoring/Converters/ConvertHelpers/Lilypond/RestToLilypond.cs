using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.Lilypond
{
    class RestToLilypond : IToLilypondConverter
    {
        public string convert(ISymbol symbol)
        {
            var rest = symbol as Rest;
            return "r" + rest.duration;
        }
    }
}
