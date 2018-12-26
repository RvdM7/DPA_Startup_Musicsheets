using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain
{
    class Meta : ISymbol
    {
        public int bpm { get; set; }
        public int beatNote { get; set; }
        public int beatsPerBar { get; set; }
        public bool changed = true;

        public Meta(int bpm, int beatNote, int beatsPerBar)
        {
            this.bpm = bpm;
            this.beatNote = beatNote;
            this.beatsPerBar = beatsPerBar;
        }

        void setChanged(bool setter)
        {
            changed = setter;
        }
    }
}
