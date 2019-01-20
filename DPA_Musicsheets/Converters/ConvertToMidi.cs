using DPA_Musicsheets.Converters.ConvertHelpers.Midi;
using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Converters
{
    class ConvertToMidi : IConverter<ISymbol>
    {
        private Dictionary<Type, IToMidiConverter> converters = new Dictionary<Type, IToMidiConverter>();

        public struct ConvertVars
        {
            public Meta meta;
            public Track metaTrack;
            public int absoluteTicks;
            public Track notesTrack;
            public int division;
            public List<string> notesOrderWithCrosses;
        }

        public ConvertToMidi()
        {
            // Add converters
            converters.Add(typeof(Meta), new MetaToMidi());
            converters.Add(typeof(Note), new NoteToMidi());
        }

        public object convert(List<ISymbol> musicList)
        {
            Sequence sequence = new Sequence();
            ConvertVars convertVars = new ConvertVars
            {
                meta = musicList[0] as Meta,
                metaTrack = new Track(),
                absoluteTicks = 0,
                notesTrack = new Track(),
                division = sequence.Division,
                notesOrderWithCrosses = new List<string>() { "c", "cis", "d", "dis", "e", "f", "fis", "g", "gis", "a", "ais", "b" }
            };

            sequence.Add(convertVars.metaTrack);

            // Calculate tempo
            int speed = (60000000 / convertVars.meta.bpm);
            byte[] tempo = new byte[3];
            tempo[0] = (byte)((speed >> 16) & 0xff);
            tempo[1] = (byte)((speed >> 8) & 0xff);
            tempo[2] = (byte)(speed & 0xff);
            convertVars.metaTrack.Insert(0 /* Insert at 0 ticks*/, new MetaMessage(MetaType.Tempo, tempo));

            sequence.Add(convertVars.notesTrack);

            foreach (var symbol in musicList)
            {
                if (converters.ContainsKey(symbol.GetType()))
                {
                    converters[symbol.GetType()].convert(symbol, ref convertVars);
                }
            }

            return sequence;
        }
    }
}
