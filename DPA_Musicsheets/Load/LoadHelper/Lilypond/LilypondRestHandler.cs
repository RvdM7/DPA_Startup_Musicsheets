using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
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
