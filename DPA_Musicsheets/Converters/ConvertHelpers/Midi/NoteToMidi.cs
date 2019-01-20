
namespace DPA_Musicsheets.Converters.ConvertHelpers.Midi
{
    class NoteToMidi : IToMidiConverter
    {
        public void convert()
        {/*
            Note note = musicalSymbol as Note;

            // Calculate duration
            double absoluteLength = 1.0 / (double)note.duration;
            absoluteLength += (absoluteLength / 2.0) * note.NumberOfDots;

            double relationToQuartNote = _beatNote / 4.0;
            double percentageOfBeatNote = (1.0 / _beatNote) / absoluteLength;
            double deltaTicks = (sequence.Division / relationToQuartNote) / percentageOfBeatNote;

            // Calculate height
            int noteHeight = notesOrderWithCrosses.IndexOf(note.Step.ToLower()) + ((note.Octave + 1) * 12);
            noteHeight += note.Alter;
            notesTrack.Insert(absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)); // Data2 = volume

            absoluteTicks += (int)deltaTicks;
            notesTrack.Insert(absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume
        */
        }
    }
}
