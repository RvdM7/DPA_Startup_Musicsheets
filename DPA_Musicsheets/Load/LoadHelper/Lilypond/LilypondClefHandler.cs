using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;
using DPA_Musicsheets.Domain.Clefs;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
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
                    addSymbol = vars.meta.isReady() ? vars.meta.clone() : addSymbol;
                }
                else if (value == "bass")
                {
                    vars.meta.clef = new Bass();
                    addSymbol = vars.meta.isReady() ? vars.meta.clone() : addSymbol;
                }
            }
        }
    }
}
