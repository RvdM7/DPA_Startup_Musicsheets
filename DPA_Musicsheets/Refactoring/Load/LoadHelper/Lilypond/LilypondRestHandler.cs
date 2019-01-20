using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    class LilypondRestHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            int duration = int.Parse(Regex.Match(value, @"\d+").Value);
            addSymbol = new Rest(duration);
        }
    }
}
