using System;
using System.Collections.Generic;
using DPA_Musicsheets.Domain;
using DPA_Musicsheets.Domain.Clefs;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Converters.ConvertHelpers.PSAM
{
    class MetaToPSAM : IToPSAM
    {
        private Dictionary<Type, ClefType> clefs = new Dictionary<Type, ClefType>();

        public MetaToPSAM()
        {
            // Add clefs
            clefs.Add(typeof(Treble), ClefType.GClef);
            clefs.Add(typeof(Bass), ClefType.FClef);
        }

        public void convert(ISymbol symbol, ref List<MusicalSymbol> musicalSymbols)
        {
            var metaSymbol = symbol as Meta;

            if (!metaSymbol.isReady()) { return; }
            ClefType clefType = clefs[metaSymbol.clef.GetType()];
            musicalSymbols.Add(new Clef(clefType, 2));
            musicalSymbols.Add(new TimeSignature(TimeSignatureType.Numbers, Convert.ToUInt32(metaSymbol.beatNote), Convert.ToUInt32(metaSymbol.beatsPerBar)));
        }
    }
}
