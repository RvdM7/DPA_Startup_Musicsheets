using System;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Midi
{
    class MetaToMidi : IToMidiConverter
    {
        public void convert()
        {/*
            byte[] timeSignature = new byte[4];
            timeSignature[0] = (byte)_beatsPerBar;
            timeSignature[1] = (byte)(Math.Log(_beatNote) / Math.Log(2));
            metaTrack.Insert(absoluteTicks, new MetaMessage(MetaType.TimeSignature, timeSignature));*/
        }
    }
}
