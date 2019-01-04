using DPA_Musicsheets.Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Converters
{
    class LoadIntoLilypond
    {
        public string LoadIntoLilypondString(List<ISymbol> symbols)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (ISymbol symbol in symbols)
            {

            }

            return stringBuilder.ToString();
        }
    }
}
