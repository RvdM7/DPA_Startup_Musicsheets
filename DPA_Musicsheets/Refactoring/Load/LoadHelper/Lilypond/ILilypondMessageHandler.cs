using DPA_Musicsheets.Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond
{
    interface ILilypondMessageHandler
    {
        void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol);
    }
}
