using System;
using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
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
                addSymbol = vars.meta.isReady() ? vars.meta.clone() : addSymbol;
            }
        }
    }
}
