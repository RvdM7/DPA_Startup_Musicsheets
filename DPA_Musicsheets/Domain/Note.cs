using DPA_Musicsheets.Domain.Additive;
using DPA_Musicsheets.Domain.Enums;

namespace DPA_Musicsheets.Domain
{
    class Note : ISymbol
    {
        public readonly NoteHeight height;
        public ICrossMole crossMole = null;
        public Dots dots = null;
        public int octaveModifier = 0;
        public int duration = 0;
        private int octave = 0;
        public int Octave
        {
            get
            {
                return octave + octaveModifier;
            }
            set
            {
                octave = value;
            }
        }

        public static Note create(NoteHeight noteHeight)
        {
            return new Note(noteHeight);
        }

        private Note(NoteHeight noteHeight)
        {
            height = noteHeight;
        }

        public override string ToString()
        {
            return $"'{height}{duration} {octave}'";
        }
    }
}
