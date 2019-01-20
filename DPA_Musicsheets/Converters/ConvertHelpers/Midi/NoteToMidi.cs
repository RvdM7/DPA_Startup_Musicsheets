using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Midi
{
    class NoteToMidi : IToMidiConverter
    {
        public void convert(ISymbol symbol, ref ConvertToMidi.ConvertVars convertVars)
        {
            Note note = symbol as Note;

            // Calculate duration
            double absoluteLength = 1.0 / note.duration;
            absoluteLength += note.dots != null ? (absoluteLength / 2.0) * note.dots.dots : (absoluteLength / 2.0);

            double relationToQuartNote = convertVars.meta.beatNote / 4.0;
            double percentageOfBeatNote = (1.0 / convertVars.meta.beatNote) / absoluteLength;
            double deltaTicks = (convertVars.division / relationToQuartNote) / percentageOfBeatNote;

            // Calculate height
            int noteHeight = convertVars.notesOrderWithCrosses.IndexOf(note.height.ToString().ToLower()) + ((note.Octave + 1) * 12);
            noteHeight += note.octaveModifier;
            convertVars.notesTrack.Insert(convertVars.absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)); // Data2 = volume

            convertVars.absoluteTicks += (int)deltaTicks;
            convertVars.notesTrack.Insert(convertVars.absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume
        }
    }
}
