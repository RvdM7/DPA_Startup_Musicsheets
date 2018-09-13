using DPA_Musicsheets.Managers;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class LoadMidiIntoLilypond
    {
        /// <summary>
        /// TODO: Create our own domain classes to be independent of external libraries/languages.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        static public string LoadMidiIntoLilypondF(Sequence sequence)
        {
            int bpm = Managers.MusicLoader._bpm;
            int beatsPerBar = Managers.MusicLoader._beatsPerBar;
            int beatNote = Managers.MusicLoader._beatNote;

            StringBuilder lilypondContent = new StringBuilder();
            lilypondContent.AppendLine("\\relative c' {");
            lilypondContent.AppendLine("\\clef treble");

            int previousMidiKey = 60; // Central C;
            int division = sequence.Division;
            int previousNoteAbsoluteTicks = 0;
            double percentageOfBarReached = 0;
            bool startedNoteIsClosed = true;

            for (int i = 0; i < sequence.Count(); i++)
            {
                Track track = sequence[i];

                foreach (var midiEvent in track.Iterator())
                {
                    IMidiMessage midiMessage = midiEvent.MidiMessage;
                    // TODO: Split this switch statements and create separate logic.
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
                                    lilypondContent.AppendLine($"\\time {beatNote}/{beatsPerBar}");
                                    break;
                                case MetaType.Tempo:
                                    byte[] tempoBytes = metaMessage.GetBytes();
                                    int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
                                    bpm = 60000000 / tempo;
                                    lilypondContent.AppendLine($"\\tempo 4={bpm}");
                                    break;
                                case MetaType.EndOfTrack:
                                    if (previousNoteAbsoluteTicks > 0)
                                    {
                                        // Finish the last notelength.
                                        double percentageOfBar;
                                        lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar));
                                        lilypondContent.Append(" ");

                                        percentageOfBarReached += percentageOfBar;
                                        if (percentageOfBarReached >= 1)
                                        {
                                            lilypondContent.AppendLine("|");
                                            percentageOfBar = percentageOfBar - 1;
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
                                    lilypondContent.Append(MidiToLilyHelper.GetLilyNoteName(previousMidiKey, channelMessage.Data1));

                                    previousMidiKey = channelMessage.Data1;
                                    startedNoteIsClosed = false;
                                }
                                else if (!startedNoteIsClosed)
                                {
                                    // Finish the previous note with the length.
                                    double percentageOfBar;
                                    lilypondContent.Append(MidiToLilyHelper.GetLilypondNoteLength(previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, division, beatNote, beatsPerBar, out percentageOfBar));
                                    previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;
                                    lilypondContent.Append(" ");

                                    percentageOfBarReached += percentageOfBar;
                                    if (percentageOfBarReached >= 1)
                                    {
                                        lilypondContent.AppendLine("|");
                                        percentageOfBarReached -= 1;
                                    }
                                    startedNoteIsClosed = true;
                                }
                                else
                                {
                                    lilypondContent.Append("r");
                                }
                            }
                            break;
                    }
                }
            }

            lilypondContent.Append("}");

            return lilypondContent.ToString();
        }
    }
}
