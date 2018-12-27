using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Additive;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper
{
    /// <summary>
    /// TODO: Static classes with static helper methods are not done. Can a better domain class help with this?
    /// </summary>
    class MidiHelper
    {
        public Note setNoteLength(int absoluteTicks, int nextNoteAbsoluteTicks, int division, int beatNote, int beatsPerBar, out double percentageOfBar, Note note)
        {
            int duration = 0;
            int dots = 0;

            double deltaTicks = nextNoteAbsoluteTicks - absoluteTicks;

            if (deltaTicks <= 0)
            {
                percentageOfBar = 0;
                return note;
            }

            double percentageOfBeatNote = deltaTicks / division;
            percentageOfBar = (1.0 / beatsPerBar) * percentageOfBeatNote;

            for (int noteLength = 32; noteLength >= 1; noteLength -= 1)
            {
                double absoluteNoteLength = (1.0 / noteLength);

                if (percentageOfBar <= absoluteNoteLength)
                {
                    if (noteLength < 2)
                        noteLength = 2;

                    int subtractDuration;

                    if (noteLength == 32)
                        subtractDuration = 32;
                    else if (noteLength >= 16)
                        subtractDuration = 16;
                    else if (noteLength >= 8)
                        subtractDuration = 8;
                    else if (noteLength >= 4)
                        subtractDuration = 4;
                    else
                        subtractDuration = 2;

                    if (noteLength >= 17)
                        duration = 32;
                    else if (noteLength >= 9)
                        duration = 16;
                    else if (noteLength >= 5)
                        duration = 8;
                    else if (noteLength >= 3)
                        duration = 4;
                    else
                        duration = 2;

                    double currentTime = 0;

                    while (currentTime < (noteLength - subtractDuration))
                    {
                        var addtime = 1 / ((subtractDuration / beatNote) * Math.Pow(2, dots));
                        if (addtime <= 0) break;
                        currentTime += addtime;
                        if (currentTime <= (noteLength - subtractDuration))
                        {
                            dots++;
                        }
                        if (dots >= 4) break;
                    }

                    break;
                }
            }
            note.dots.dots = dots;
            note.duration = duration;
            return note;
        }

        public Note getNoteWithHeight(int previousMidiKey, int midiKey)
        {
            Note note = null;
            int octave = (midiKey / 12) - 1;
            switch (midiKey % 12)
            {
                case 0:
                    note = new Note(NoteHeight.c);
                    //note.height = NoteHeight.c;
                    break;
                case 1:
                    note = new Note(NoteHeight.c);
                    //note.height = NoteHeight.c;
                    //note = new FlatSharpDecorator(note);
                    break;
                case 2:
                    note = new Note(NoteHeight.d);
                    //note.height = NoteHeight.d;
                    break;
                case 3:
                    note = new Note(NoteHeight.d);
                    //note.height = NoteHeight.d;
                    //note = new FlatSharpDecorator(note);
                    break;
                case 4:
                    note = new Note(NoteHeight.e);
                    //note.height = NoteHeight.e;
                    break;
                case 5:
                    note = new Note(NoteHeight.f);
                    //note.height = NoteHeight.f;
                    break;
                case 6:
                    note = new Note(NoteHeight.f);
                    //note.height = NoteHeight.f;
                    //note = new FlatSharpDecorator(note);
                    break;
                case 7:
                    note = new Note(NoteHeight.g);
                    //note.height = NoteHeight.g;
                    break;
                case 8:
                    note = new Note(NoteHeight.g);
                    //note.height = NoteHeight.g;
                    //note = new FlatSharpDecorator(note);
                    break;
                case 9:
                    note = new Note(NoteHeight.a);
                    //note.height = NoteHeight.a;
                    break;
                case 10:
                    note = new Note(NoteHeight.a);
                    //note.height = NoteHeight.a;
                    //note = new FlatSharpDecorator(note);
                    break;
                case 11:
                    note = new Note(NoteHeight.b);
                    //note.height = NoteHeight.b;
                    break;
            }
            if (note == null)
            {
                throw new NotSupportedException();
            }

            int distance = midiKey - previousMidiKey;
            int test = 0;
            while (distance < -6)
            {
                test--;
                distance += 8;
            }
            while (distance > 6)
            {
                test++;
                distance -= 8;
            }
            note.octave = test;

            return note;
        }
    }
}
