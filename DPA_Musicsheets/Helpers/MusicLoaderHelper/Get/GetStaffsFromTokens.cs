using DPA_Musicsheets.Models;
using PSAMControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class GetStaffsFromTokens
    {
        public static IEnumerable<MusicalSymbol> GetStaffsFromTokensF(LinkedList<LilypondToken> tokens)
        {
            List<Char> notesorder = Managers.MusicLoader.notesorder;
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            Clef currentClef = null;
            int previousOctave = 4;
            char previousNote = 'c';
            bool inRepeat = false;
            bool inAlternative = false;
            int alternativeRepeatNumber = 0;

            LilypondToken currentToken = tokens.First();
            while (currentToken != null)
            {
                // TODO: There are a lot of switches based on LilypondTokenKind, can't those be eliminated en delegated?
                // HINT: Command, Decorator, Factory etc.

                // TODO: Repeats are somewhat weirdly done. Can we replace this with the COMPOSITE pattern?
                switch (currentToken.TokenKind)
                {
                    case LilypondTokenKind.Unknown:
                        break;
                    case LilypondTokenKind.Repeat:
                        inRepeat = true;
                        symbols.Add(new Barline() { RepeatSign = RepeatSignType.Forward });
                        break;
                    case LilypondTokenKind.SectionEnd:
                        if (inRepeat && currentToken.NextToken?.TokenKind != LilypondTokenKind.Alternative)
                        {
                            inRepeat = false;
                            symbols.Add(new Barline() { RepeatSign = RepeatSignType.Backward, AlternateRepeatGroup = alternativeRepeatNumber });
                        }
                        else if (inAlternative && alternativeRepeatNumber == 1)
                        {
                            alternativeRepeatNumber++;
                            symbols.Add(new Barline() { RepeatSign = RepeatSignType.Backward, AlternateRepeatGroup = alternativeRepeatNumber });
                        }
                        else if (inAlternative && currentToken.NextToken.TokenKind == LilypondTokenKind.SectionEnd)
                        {
                            inAlternative = false;
                            alternativeRepeatNumber = 0;
                        }
                        break;
                    case LilypondTokenKind.SectionStart:
                        if (inAlternative && currentToken.PreviousToken.TokenKind != LilypondTokenKind.SectionEnd)
                        {
                            alternativeRepeatNumber++;
                            symbols.Add(new Barline() { AlternateRepeatGroup = alternativeRepeatNumber });
                        }
                        break;
                    case LilypondTokenKind.Alternative:
                        inAlternative = true;
                        inRepeat = false;
                        currentToken = currentToken.NextToken; // Skip the first bracket open.
                        break;
                    case LilypondTokenKind.Note:
                        // Tied
                        // TODO: A tie, like a dot and cross or mole are decorations on notes. Is the DECORATOR pattern of use here?
                        NoteTieType tie = NoteTieType.None;
                        if (currentToken.Value.StartsWith("~"))
                        {
                            tie = NoteTieType.Stop;
                            var lastNote = symbols.Last(s => s is Note) as Note;
                            if (lastNote != null) lastNote.TieType = NoteTieType.Start;
                            currentToken.Value = currentToken.Value.Substring(1);
                        }
                        // Length
                        int noteLength = Int32.Parse(Regex.Match(currentToken.Value, @"\d+").Value);
                        // Crosses and Moles
                        int alter = 0;
                        alter += Regex.Matches(currentToken.Value, "is").Count;
                        alter -= Regex.Matches(currentToken.Value, "es|as").Count;
                        // Octaves
                        int distanceWithPreviousNote = notesorder.IndexOf(currentToken.Value[0]) - notesorder.IndexOf(previousNote);
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
                        previousOctave += currentToken.Value.Count(c => c == '\'');
                        previousOctave -= currentToken.Value.Count(c => c == ',');

                        previousNote = currentToken.Value[0];

                        var note = new Note(currentToken.Value[0].ToString().ToUpper(), alter, previousOctave, (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, tie, new List<NoteBeamType>() { NoteBeamType.Single });
                        note.NumberOfDots += currentToken.Value.Count(c => c.Equals('.'));

                        symbols.Add(note);
                        break;
                    case LilypondTokenKind.Rest:
                        var restLength = Int32.Parse(currentToken.Value[1].ToString());
                        symbols.Add(new Rest((MusicalSymbolDuration)restLength));
                        break;
                    case LilypondTokenKind.Bar:
                        symbols.Add(new Barline() { AlternateRepeatGroup = alternativeRepeatNumber });
                        break;
                    case LilypondTokenKind.Clef:
                        currentToken = currentToken.NextToken;
                        if (currentToken.Value == "treble")
                            currentClef = new Clef(ClefType.GClef, 2);
                        else if (currentToken.Value == "bass")
                            currentClef = new Clef(ClefType.FClef, 4);
                        else
                            throw new NotSupportedException($"Clef {currentToken.Value} is not supported.");

                        symbols.Add(currentClef);
                        break;
                    case LilypondTokenKind.Time:
                        currentToken = currentToken.NextToken;
                        var times = currentToken.Value.Split('/');
                        symbols.Add(new TimeSignature(TimeSignatureType.Numbers, UInt32.Parse(times[0]), UInt32.Parse(times[1])));
                        break;
                    case LilypondTokenKind.Tempo:
                        // Tempo not supported
                        break;
                    default:
                        break;
                }
                currentToken = currentToken.NextToken;
            }

            return symbols;
        }
    }
}
