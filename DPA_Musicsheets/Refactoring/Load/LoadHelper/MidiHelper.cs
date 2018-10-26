using DPA_Musicsheets.Refactoring.Tokens;
using DPA_Musicsheets.Refactoring.Tokens.NoteDecorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper
{
    class MidiHelper
    {
        public INote setNoteLength(int absoluteTicks, int nextNoteAbsoluteTicks, int division, int beatNote, int beatsPerBar, out double percentageOfBar, INote note)
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
            note.setLength(duration);

            //note.dot = dots;
            if (dots != 0)
            {
                return new DotDecorator(note, dots);
            }
            else
            {
                return note;
            }

            //return duration + new String('.', dots);
        }

        public INote setNoteHeight(int previousMidiKey, int midiKey, INote note)
        {
            int octave = (midiKey / 12) - 1;
            //string name = "";
            switch (midiKey % 12)
            {
                case 0:
                    note.setHeight('c');
                    //name = "c";
                    break;
                case 1:
                    note.setHeight('c');
                    note = new FlatSharpDecorator(note);
                    //note.sharp = true;
                    //name = "cis";
                    break;
                case 2:
                    note.setHeight('d');
                    //name = "d";
                    break;
                case 3:
                    note.setHeight('d');
                    note = new FlatSharpDecorator(note);
                    //name = "dis";
                    break;
                case 4:
                    note.setHeight('e');
                    //name = "e";
                    break;
                case 5:
                    note.setHeight('f');
                    //name = "f";
                    break;
                case 6:
                    note.setHeight('f');
                    note = new FlatSharpDecorator(note);
                    //name = "fis";
                    break;
                case 7:
                    note.setHeight('g');
                    //name = "g";
                    break;
                case 8:
                    note.setHeight('g');
                    note = new FlatSharpDecorator(note);
                    //name = "gis";
                    break;
                case 9:
                    note.setHeight('a');
                    //name = "a";
                    break;
                case 10:
                    note.setHeight('a');
                    note = new FlatSharpDecorator(note);
                    //name = "ais";
                    break;
                case 11:
                    note.setHeight('b');
                    //name = "b";
                    break;
            }

            int distance = midiKey - previousMidiKey;
            int test = 0;
            while (distance < -6)
            {
                test++;

                //note.komma += 1;
                //name += ",";
                distance += 8;
            }
            note = test > 0 ? new OctaveDecorator(note, false, test) : note;
            test = 0;
            while (distance > 6)
            {
                test++;
                //note = new OctaveDecorator(note);
                //note.apostrof += 1;
                //name += "'";
                distance -= 8;
            }
            note = test > 0 ? new OctaveDecorator(note, true, test) : note;

            return note;
        }
    }
}
