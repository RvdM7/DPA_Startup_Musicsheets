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
                    Domain.Note currentToken = (Domain.Note)test[i];
                    /*
                    var PSAMNote = new PSAMControlLibrary.Note(note.height.ToString().ToUpper(), 0, previousOctave + note.octave, (MusicalSymbolDuration)note.duration, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    PSAMNote.NumberOfDots += note.dots.dots;
                    symbols.Add(PSAMNote);
                    */
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
                    //int noteLength = Int32.Parse(Regex.Match(currentToken.Value, @"\d+").Value);
                    // Crosses and Moles
                    /*
                    int alter = 0;
                    alter += Regex.Matches(currentToken.Value, "is").Count;
                    alter -= Regex.Matches(currentToken.Value, "es|as").Count;*/
                    // Octaves
                    int distanceWithPreviousNote = notesorder.IndexOf((char)currentToken.height) - notesorder.IndexOf(previousNote);
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
                    previousOctave += currentToken.octave;//currentToken.Value.Count(c => c == '\'');
                    //previousOctave -= //currentToken.Value.Count(c => c == ',');

                    previousNote = (char)currentToken.height;

                    char height = (char)currentToken.height;
                    var note = new PSAMControlLibrary.Note(height.ToString().ToUpper(), 0, previousOctave, (MusicalSymbolDuration)currentToken.duration, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    note.NumberOfDots += currentToken.dots.dots;

                    symbols.Add(note);
                }
                else if (symbol is Meta)
                {
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
