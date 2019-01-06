using DPA_Musicsheets.Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.Lilypond
{
    interface IToLilypondConverter
    {
        string convert(ISymbol symbol);
    }
}
