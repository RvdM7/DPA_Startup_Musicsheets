using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Load.LoadHelper;
using DPA_Musicsheets.Refactoring.Load.LoadHelper.Midi;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadMidi : ILoader
    {
        private Dictionary<MessageType, IMidiMessageHandler> strategies = new Dictionary<MessageType, IMidiMessageHandler>();
        public static MidiHelper midiHelper = new MidiHelper();

        public struct LoadVars
        {
            public Meta meta;
            public Sequence MidiSequence;
            public int division;
            public int previousNoteAbsoluteTicks;
            public double percentageOfBarReached;
            public int previousMidiKey;
            public bool startedNoteIsClosed;
            public LoadVars(string fileName)
            {
                meta = new Meta();
                MidiSequence = new Sequence();
                MidiSequence.Load(fileName);
                division = MidiSequence.Division;
                previousNoteAbsoluteTicks = 0;
                percentageOfBarReached = 0;
                previousMidiKey = 60; // Central C
                startedNoteIsClosed = true;
            }
        }

        public LoadMidi()
        {
            // Add strategies
            strategies.Add(MessageType.Meta, new MidiMetaMessageHandler());
            strategies.Add(MessageType.Channel, new MidiChannelMessageHandler());
        }

        public override List<ISymbol> loadMusic()
        {
            LoadVars vars = new LoadVars(fileName);
            List<ISymbol> symbols = new List<ISymbol>();
            ISymbol addNote = null;
            vars.meta.clef = Domain.Enums.Clefs.treble;

            for (int i = 0; i < vars.MidiSequence.Count(); i++)
            {
                Track track = vars.MidiSequence[i];

                foreach (var midiEvent in track.Iterator())
                {
                    // TODO : Split this switch statements and create separate logic.
                    // We want to split this so that we can expand our functionality later with new keywords for example.
                    // Hint: Command pattern? Strategies? Factory method?
                    if (strategies.ContainsKey(midiEvent.MidiMessage.MessageType))
                    {
                        strategies[midiEvent.MidiMessage.MessageType].handleMessage(midiEvent, ref vars, ref addNote, ref symbols);
                    }
                }
            }
            return symbols;
        }
    }
}
