using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Load.LoadHelper;
using DPA_Musicsheets.Refactoring.Tokens;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadMidi : ILoader
    {
        private string fileName;
        private MidiHelper mh = new MidiHelper();


        public LoadMidi(string fileName)
        {
            this.fileName = fileName;
        }

        public List<IToken> loadMusic()
        {
            //throw new NotImplementedException();

            List<IToken> test = new List<IToken>();
            INote addNote = new Note();

            int[] array = new int[3] { 4, 120, 0 };
            int beatNote = 4;
            int bpm = 120;
            int beatsPerBar = 0;
            MetaToken mt = new MetaToken(bpm, beatNote, beatsPerBar);

            Sequence MidiSequence = new Sequence();
            MidiSequence.Load(fileName);

            int previousMidiKey = 60; // Central C;
            int division = MidiSequence.Division;
            int previousNoteAbsoluteTicks = 0;
            double percentageOfBarReached = 0;
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
                                    beatNote = timeSignatureBytes[0];
                                    beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
                                    mt[1] = beatNote;
                                    mt[2] = beatsPerBar;
                                    //mt.beatNote = beatNote;
                                    //mt.beatsPerBar = beatsPerBar;
                                    //lilypondContent.AppendLine($"\\time {beatNote}/{beatsPerBar}");
                                    break;
                                case MetaType.Tempo:
                                    byte[] tempoBytes = metaMessage.GetBytes();
                                    int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
                                    bpm = 60000000 / tempo;
                                    mt[0] = bpm;
                                    //mt.bpm = bpm;
                                    //lilypondContent.AppendLine($"\\tempo 4={bpm}");
                                    break;
                                case MetaType.EndOfTrack:

                                    if (previousNoteAbsoluteTicks > 0)
                                    {
                                        // Finish the last notelength.
                                        double percentageOfBar;
                                        addNote = mh.setNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar, addNote);

                                        //lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar));
                                        //lilypondContent.Append(" ");

                                        percentageOfBarReached += percentageOfBar;
                                        if (percentageOfBarReached >= 1)
                                        {
                                            //lilypondContent.AppendLine("|");
                                            percentageOfBar = percentageOfBar - 1;
                                        }
                                    }
                                    test.Add(addNote);
                                    addNote = null;
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
                                    addNote = mh.setNoteHeight(previousMidiKey, channelMessage.Data1, addNote);
                                    //lilypondContent.Append(MidiToLilyHelper.GetLilyNoteName(previousMidiKey, channelMessage.Data1));

                                    previousMidiKey = channelMessage.Data1;
                                    startedNoteIsClosed = false;
                                }
                                else if (!startedNoteIsClosed)
                                {
                                    // Finish the previous note with the length.
                                    double percentageOfBar;
                                    addNote = mh.setNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar, addNote);
                                    //lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar));
                                    previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                                    //lilypondContent.Append(" ");

                                    percentageOfBarReached += percentageOfBar;
                                    if (percentageOfBarReached >= 1)
                                    {
                                        //lilypondContent.AppendLine("|");
                                        percentageOfBarReached -= 1;
                                    }
                                    startedNoteIsClosed = true;

                                    if (mt.changed)
                                    {
                                        test.Add(mt);
                                        mt.updated();
                                    }
                                    test.Add(addNote);
                                    addNote = new Note();
                                }
                                else
                                {
                                    test.Add(new Note());
                                    // add rust
                                    //lilypondContent.Append("r");
                                }
                            }
                            break;
                    }
                }
            }

            return test;
        }
    }
}
