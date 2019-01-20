using DPA_Musicsheets.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DPA_Musicsheets.Converters.ConvertHelpers.Lilypond;

namespace DPA_Musicsheets.Converters
{
    class ConvertToLilypond : IConverter<ISymbol>
    {
        private Dictionary<Type, IToLilypondConverter> converters = new Dictionary<Type, IToLilypondConverter>();

        public ConvertToLilypond()
        {
            // Add converters
            converters.Add(typeof(Note), new NoteToLilypond());
            converters.Add(typeof(Bar), new BarToLilypond());
            converters.Add(typeof(Meta), new MetaToLilypond());
            converters.Add(typeof(Rest), new RestToLilypond());
        }

        public object convert(List<ISymbol> musicList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("\\relative c'");

            foreach (ISymbol symbol in musicList)
            {
                var type = symbol.GetType();
                if (converters.ContainsKey(type))
                {
                    stringBuilder.Append(converters[type].convert(symbol));
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
