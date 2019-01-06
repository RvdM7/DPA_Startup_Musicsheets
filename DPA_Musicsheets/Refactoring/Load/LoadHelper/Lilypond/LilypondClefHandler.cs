using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Clefs;
using DPA_Musicsheets.Refactoring.Domain.Enums;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    class LilypondClefHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            if (!Regex.Match(value, @"clef").Success)
            {
                if (value == "treble")
                {
                    vars.meta.clef = new Treble();
                    addSymbol = vars.meta.clone();
                }
                else if (value == "bass")
                {
                    vars.meta.clef = new Bass();
                    addSymbol = vars.meta.clone();
                }
            }
        }
    }
}
