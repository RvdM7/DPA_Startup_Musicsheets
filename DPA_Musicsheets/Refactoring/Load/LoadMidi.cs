using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Load.LoadHelper;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadMidi : ILoader
    {
        private string fileName;
        private MidiHelper midiHelper = new MidiHelper();


        public LoadMidi(string fileName)
        {
            this.fileName = fileName;
        }

        public List<ISymbol> loadMusic()
        {
            //throw new NotImplementedException();
            Sequence MidiSequence = new Sequence();
            MidiSequence.Load(fileName);

            Meta meta = new Meta(120, 4, 0);
            Note addNote = new Note();

            List<ISymbol> test = new List<ISymbol>();

            int previousMidiKey = 60; // Central C;
            int previousNoteAbsoluteTicks = 0;
            double percentageOfBarReached = 0;
            int division = MidiSequence.Division;
            bool startedNoteIsClosed = true;

            for (int i = 0; i < MidiSequence.Count(); i++)
            {
                Track track = MidiSequence[i];

                foreach (var midiEvent in track.Iterator())
                {
                    IMidiMessage midiMessage = midiEvent.MidiMessage;
                    // TODO : Split this switch statements and create separate logic.
                    // We want to split this so that we can expand our functionality later with new keywords for example.
                    // Hint: Command pattern? Strategies? Factory method?
                    switch (midiMessage.MessageType)
                    {
                        case MessageType.Meta:
                            var metaMessage = midiMessage as MetaMessage;
                            switch (metaMessage.MetaType)
                            {
                                case MetaType.TimeSignature:
                                    byte[] timeSignatureBytes = metaMessage.GetBytes();
                                    meta.beatNote = timeSignatureBytes[0];
                                    meta.beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
                                    break;
                                case MetaType.Tempo:
                                    byte[] tempoBytes = metaMessage.GetBytes();
                                    int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
                                    meta.bpm = 60000000 / tempo;
                                    break;
                                case MetaType.EndOfTrack:

                                    if (previousNoteAbsoluteTicks > 0)
                                    {
                                        // Finish the last notelength.
                                        double percentageOfBar;
                                        addNote = midiHelper.setNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, meta.beatNote, meta.beatsPerBar, out percentageOfBar, addNote);

                                        percentageOfBarReached += percentageOfBar;
                                        if (percentageOfBarReached >= 1)
                                        {
                                            percentageOfBar = percentageOfBar - 1;
                                            test.Add(new Bar());
                                        }
                                    }
                                    break;
                                default: break;
                            }
                            break;
                        case MessageType.Channel:
                            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
                            if (channelMessage.Command == ChannelCommand.NoteOn)
                            {
                                if (channelMessage.Data2 > 0) // Data2 = loudness
                                {
                                    // Append the new note.
                                    addNote = new Note();
                                    addNote = midiHelper.setNoteHeight(previousMidiKey, channelMessage.Data1, addNote);

                                    previousMidiKey = channelMessage.Data1;
                                    startedNoteIsClosed = false;
                                }
                                else if (!startedNoteIsClosed)
                                {
                                    // Finish the previous note with the length.
                                    double percentageOfBar;
                                    addNote = midiHelper.setNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, meta.beatNote, meta.beatsPerBar, out percentageOfBar, addNote);
                                    previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;

                                    percentageOfBarReached += percentageOfBar;
                                    if (percentageOfBarReached >= 1)
                                    {
                                        percentageOfBarReached -= 1;
                                        test.Add(new Bar());
                                    }
                                    startedNoteIsClosed = true;

                                    test.Add(meta);
                                    test.Add(addNote);
                                    addNote = new Note();
                                }
                                else
                                {
                                    // add rust
                                    //lilypondContent.Append("r");
                                }
                            }
                            break;
                        default: break;
                    }
                }
            }

            return test;
        }
    }
}
