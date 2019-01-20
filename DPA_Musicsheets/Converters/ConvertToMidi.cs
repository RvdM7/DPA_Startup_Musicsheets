using DPA_Musicsheets.Converters.ConvertHelpers.Midi;
using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Converters
{
    class ConvertToMidi : IConverter<ISymbol>
    {
        private Dictionary<Type, IToMidiConverter> conveters = new Dictionary<Type, IToMidiConverter>();

        public struct convertVars
        {
            public Meta meta;
            public int tempo;
        }

        public ConvertToMidi()
        {
            // Add converters
        }

        public object convert(List<ISymbol> musicList)
        {
            List<string> notesOrderWithCrosses = new List<string>() { "c", "cis", "d", "dis", "e", "f", "fis", "g", "gis", "a", "ais", "b" };
            int absoluteTicks = 0;

            Sequence sequence = new Sequence();
            /*
            Track metaTrack = new Track();
            sequence.Add(metaTrack);
            
            // Calculate tempo
            int speed = (60000000 / _bpm);
            byte[] tempo = new byte[3];
            tempo[0] = (byte)((speed >> 16) & 0xff);
            tempo[1] = (byte)((speed >> 8) & 0xff);
            tempo[2] = (byte)(speed & 0xff);
            metaTrack.Insert(0 /* Insert at 0 ticks*, new MetaMessage(MetaType.Tempo, tempo)); 

             Track notesTrack = new Track();
            sequence.Add(notesTrack);

            foreach (var symbol in musicList)
            {

            }
            */
            return sequence;
        }
    }
}
