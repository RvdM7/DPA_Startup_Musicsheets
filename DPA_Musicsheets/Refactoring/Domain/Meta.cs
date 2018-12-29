using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain
{
    class Meta : ISymbol
    {
        public int bpm = 0;
        public int beatNote = 0;
        public int beatsPerBar = 0;
        public bool changed = true;
        public string clef;

        public Meta() { }

        public Meta(Meta meta)
        {
            bpm = meta.bpm;
            beatNote = meta.beatNote;
            beatsPerBar = meta.beatsPerBar;
        }

        void setChanged(bool setter)
        {
            changed = setter;
        }

        public bool isReady() => bpm != 0 && beatNote != 0 && beatsPerBar != 0;

        public override string ToString()
        {
            return $"{bpm}-{beatNote}-{beatsPerBar}";
        }
    }
}
