using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
{
    class LilypondStaffHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            if (!Regex.Match(value, @"relative").Success)
            {
                vars.previousNoteHeight = value[0];
            }
        }
    }
}
