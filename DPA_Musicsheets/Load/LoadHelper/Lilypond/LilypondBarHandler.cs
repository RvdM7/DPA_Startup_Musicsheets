﻿using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
{
    class LilypondBarHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            addSymbol = SymbolFactory.getBar();
        }
    }
}
