using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Domain.Clefs;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Refactoring.Converters
{
    class ConvertToPSAM : IConverter<ISymbol>
    {
        private Dictionary<Type, ClefType> clefs = new Dictionary<Type, ClefType>();

        public ConvertToPSAM()
        {
            // Add clefs
            clefs.Add(typeof(Treble), ClefType.GClef);
            clefs.Add(typeof(Bass), ClefType.FClef);
        }

        object IConverter<ISymbol>.convert(List<ISymbol> symbols)
        {
            List<MusicalSymbol> musicalSymbols = new List<MusicalSymbol>();

            foreach (ISymbol symbol in symbols)
            {
                if (symbol is Domain.Note note)
                {
                    // Length
                    int noteLength = note.duration;

                    // Crosses and Moles
                    int alter = note.crossMole != null ? note.crossMole.getModifier() : 0;

                    var PSAMNote = new PSAMControlLibrary.Note(note.height.ToString().ToUpper(), alter, note.Octave, (MusicalSymbolDuration)noteLength, NoteStemDirection.Up, NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });
                    PSAMNote.NumberOfDots += note.dots != null ? note.dots.dots : 0;

                    musicalSymbols.Add(PSAMNote);
                }
                else if (symbol is Meta metaSymbol)
                {
                    if (!metaSymbol.isReady()) { continue; }
                    ClefType clefType = clefs[metaSymbol.clef.GetType()];
                    musicalSymbols.Add(new Clef(clefType, 2));
                    musicalSymbols.Add(new TimeSignature(TimeSignatureType.Numbers, Convert.ToUInt32(metaSymbol.beatNote), Convert.ToUInt32(metaSymbol.beatsPerBar)));
                }
                else if (symbol is Bar)
                {
                    musicalSymbols.Add(new Barline());
                }
            }
            return musicalSymbols;
        }
    }
}
