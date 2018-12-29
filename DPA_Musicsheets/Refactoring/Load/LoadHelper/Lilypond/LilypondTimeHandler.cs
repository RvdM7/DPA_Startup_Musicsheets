using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    class LilypondTimeHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            if (!Regex.Match(value, @"time").Success)
            {
                string[] time = value.Split('/');
                vars.meta.beatNote = int.Parse(time[0]);
                vars.meta.beatsPerBar = int.Parse(time[1]);
                addSymbol = new Meta(vars.meta);
            }
        }
    }
}
