using DPA_Musicsheets.Refactoring.Tokens.NoteDecorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Tokens
{
    class MetaToken : IToken
    {
        public int bpm { get; set; }
        public int beatNote { get; set; }
        public int beatsPerBar { get; set; }
        public int[] array;

        public bool changed { get; private set; }

        public MetaToken(int bpm, int beatNote, int beatsPerBar)
        {
            this.bpm = bpm;
            this.beatNote = beatNote;
            this.beatsPerBar = beatsPerBar;
            array = new int[3] { bpm, beatNote, beatsPerBar };
        }

        public int this[int i]
        {
            get
            {
                return this.array[i];
            }
            set
            {
                if (value != this.array[i])
                {
                    this.array[i] = value;
                    changed = true;
                }
            }
        }

        public void updated()
        {
            changed = false;
        }

    }
}
