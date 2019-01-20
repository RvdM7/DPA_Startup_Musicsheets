using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
{
    class LilypondTempoHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            if (!(Regex.Match(value, @"tempo").Success))
            {
                string[] tempo = value.Split('=');
                vars.meta.bpm = int.Parse(tempo[1]);
                addSymbol = vars.meta.isReady() ? vars.meta.clone() : addSymbol;
            }
        }
    }
}
