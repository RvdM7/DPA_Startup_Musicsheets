using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Additive;

namespace DPA_Musicsheets.Refactoring.Converters.ConvertHelpers.Lilypond
{
    class NoteToLilypond : IToLilypondConverter
    {
        public string convert(ISymbol symbol)
        {
            var note = symbol as Note;

            // Height
            string returnString = $"{note.height}";

            // Crosses and Moles
            if (note.crossMole != null)
            {
                var additive = note.crossMole is Flat ? "is" : "es";
                for (int i = 0; i < note.crossMole.getModifier(); i++)
                {
                    returnString += additive;
                }
            }

            // Modifier
            if (note.octaveModifier != 0)
            {
                var modifier = note.octaveModifier < 0 ? "," : "\'";
                for (int i = 0; i < Math.Abs(note.octaveModifier); i++)
                {
                    returnString += modifier;
                }
            }

            // Duration
            returnString += note.duration;

            // Dots
            if (note.dots != null)
            {
                returnString += note.dots.dots;
            }

            return returnString;
        }
    }
}
