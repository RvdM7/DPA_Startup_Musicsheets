using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;
using System;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Midi
{
    class MetaToMidi : IToMidiConverter
    {
        public void convert(ISymbol symbol, ref ConvertToMidi.ConvertVars convertVars)
        {
            var meta = symbol as Meta;
            byte[] timeSignature = new byte[4];
            timeSignature[0] = (byte)meta.beatsPerBar;
            timeSignature[1] = (byte)(Math.Log(meta.beatNote) / Math.Log(2));
            convertVars.metaTrack.Insert(convertVars.absoluteTicks, new MetaMessage(MetaType.TimeSignature, timeSignature));
        }
    }
}
