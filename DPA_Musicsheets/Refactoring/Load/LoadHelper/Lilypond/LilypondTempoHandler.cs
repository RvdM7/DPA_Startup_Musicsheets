using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    class LilypondTempoHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            if (!(Regex.Match(value, @"tempo").Success))
            {
                string[] tempo = value.Split('=');
                vars.meta.bpm = int.Parse(tempo[1]);
                addSymbol = new Meta(vars.meta);
            }
        }
    }
}
