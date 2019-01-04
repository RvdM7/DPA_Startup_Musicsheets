using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Midi
{
    class MidiChannelMessageHandler : IMidiMessageHandler
    {
        public void handleMessage(MidiEvent midiEvent, ref LoadMidi.LoadVars vars, ref ISymbol addNote, ref List<ISymbol> symbols)
        {
            var channelMessage = midiEvent.MidiMessage as ChannelMessage;
            if (channelMessage.Command == ChannelCommand.NoteOn)
            {
                if (channelMessage.Data2 > 0) // Data2 = loudness
                {
                    // Append the new note.
                    addNote = LoadMidi.midiHelper.getNoteWithHeight(vars.previousMidiKey, channelMessage.Data1);

                    vars.previousMidiKey = channelMessage.Data1;
                    vars.startedNoteIsClosed = false;
                }
                else if (!vars.startedNoteIsClosed)
                {
                    // Finish the previous note with the length.
                    addNote = LoadMidi.midiHelper.setNoteLength(vars.previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, vars.division, vars.meta.beatNote, vars.meta.beatsPerBar, out double percentageOfBar, addNote);
                    vars.previousNoteAbsoluteTicks = midiEvent.AbsoluteTicks;

                    vars.percentageOfBarReached += percentageOfBar;
                    if (vars.percentageOfBarReached >= 1)
                    {
                        vars.percentageOfBarReached -= 1;
                        symbols.Add(new Bar());
                    }
                    vars.startedNoteIsClosed = true;

                    if (vars.meta.changed)
                    {
                        symbols.Add(vars.meta);
                        vars.meta.changed = false;
                    }
                    symbols.Add(addNote);
                    addNote = null;
                }
                else
                {
                    addNote = new Rest();
                    vars.startedNoteIsClosed = false;
                }
            }
        }
    }
}
