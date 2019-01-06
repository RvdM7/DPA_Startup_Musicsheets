using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Clefs;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.Lilypond
{
    class MetaToLilypond : IToLilypondConverter
    {
        private Dictionary<Type, string> clefs = new Dictionary<Type, string>();

        public MetaToLilypond()
        {
            // Add clefs
            clefs.Add(typeof(Treble), "treble");
            clefs.Add(typeof(Bass), "bass");
        }

        public string convert(ISymbol symbol)
        {
            var meta = symbol as Meta;

            // Clef
            string returnString = "\\clef ";
            returnString += clefs[meta.clef.GetType()] + Environment.NewLine;

            // Time
            returnString += $"\\time {meta.beatNote}/{meta.beatsPerBar}" + Environment.NewLine;

            // Tempo
            returnString += $"\\tempo 4={meta.bpm}" + Environment.NewLine;

            return returnString;
        }
    }
}
