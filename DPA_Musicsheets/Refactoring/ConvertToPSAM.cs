using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring
{
    class ConvertToPSAM : IConverter<ISymbol>
    {
        object IConverter<ISymbol>.convert(List<ISymbol> test)
        {
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            Clef currentClef = null;
            int previousOctave = 4;
            char previousNote = 'c';
            bool inRepeat = false;
            bool inAlternative = false;
            int alternativeRepeatNumber = 0;
            List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };

            symbols.Add(new PSAMControlLibrary.Clef(ClefType.GClef, 2));
            for (int i = 0; i < test.Count; i++)
            {
                ISymbol symbol = test[i];

                if (symbol is Domain.Note)
                {
                    Domain.Note note = (Domain.Note)test[i];
                    // Tied
                    // TODO: A tie, like a dot and cross or mole are decorations on notes. Is the DECORATOR pattern of use here?
                    /*NoteTieType tie = NoteTieType.None;
                    if (currentToken.Value.StartsWith("~"))
                    {
                        tie = NoteTieType.Stop;
                        var lastNote = symbols.Last(s => s is Note) as Note;
                        if (lastNote != null) lastNote.TieType = NoteTieType.Start;
                        currentToken.Value = currentToken.Value.Substring(1);
                    }*/
                    // Length
                    int noteLength = note.duration;
                    // Crosses and Moles
                    int alter = 0;
                    alter = note.octaveModifier != null ? note.octaveModifier.getModifier() : alter;
                    // Octaves
                    int distanceWithPreviousNote = notesorder.IndexOf((char)note.height) - notesorder.IndexOf(previousNote);
                    if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
                    {
                        distanceWithPreviousNote -= 7; // The number of notes in an octave
                    }
                    else if (distanceWithPreviousNote < -3)
                    {
                        distanceWithPreviousNote += 7; // The number of notes in an octave
                    }

                    if (distanceWithPreviousNote + notesorder.IndexOf(previousNote) >= 7)
                    {
                        previousOctave++;
                    }
                    else if (distanceWithPreviousNote + notesorder.IndexOf(previousNote) < 0)
                    {
                        previousOctave--;
                    }

                    // Force up or down.
                    previousOctave += note.octave;

                    previousNote = (char)note.height;

                    var PSAMNote = new PSAMControlLibrary.Note(note.height.ToString().ToUpper(), alter, previousOctave, (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    PSAMNote.NumberOfDots += note.dots != null ? note.dots.dots : 0;

                    symbols.Add(PSAMNote);
                }
                else if (symbol is Meta)
                {
                    var metaSymbol = symbol as Meta;
                    symbols.Add(new TimeSignature(TimeSignatureType.Numbers, Convert.ToUInt32(metaSymbol.beatNote), Convert.ToUInt32(metaSymbol.beatsPerBar)));
                }
                else if (symbol is Bar)
                {
                    symbols.Add(new Barline());
                }
            }
            return symbols;
        }
    }
}
