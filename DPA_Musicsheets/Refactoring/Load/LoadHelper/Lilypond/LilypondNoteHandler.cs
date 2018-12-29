using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Additive;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    class LilypondNoteHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            int count = Regex.Matches(value, @"\.").Count;

            Note note = new Note((NoteHeight)value[0])
            {
                duration = int.Parse(Regex.Match(value, @"\d+").Value),
                dots = count > 0 ? new Dots(count) : null
            };

            count = 0;
            count += Regex.Matches(value, @"is").Count;
            count -= Regex.Matches(value, @"es|as").Count;
            if (count != 0)
            {
                note.octaveModifier = count < 0 ? (IOctaveModifier)new Sharp(count) : new Flat(count);
            }

            count = 0;
            count += Regex.Matches(value, @"\'").Count;
            count -= Regex.Matches(value, @",").Count;
            if (count != 0)
            {
                note.octave = count;
            }

            addSymbol = note;
        }
    }
}
