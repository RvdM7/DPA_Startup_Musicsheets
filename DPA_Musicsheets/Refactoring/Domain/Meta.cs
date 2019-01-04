using DPA_Musicsheets.Refactoring.Domain.Enums;
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
        public Clefs clef = Clefs.none;

        void setChanged(bool setter)
        {
            changed = setter;
        }

        public bool isReady() => bpm != 0 && beatNote != 0 && beatsPerBar != 0 && clef != Clefs.none;

        public override string ToString()
        {
            return $"{bpm}-{beatNote}-{beatsPerBar}";
        }

        public Meta clone()
        {
            Meta meta = new Meta
            {
                bpm = bpm,
                beatNote = beatNote,
                beatsPerBar = beatsPerBar,
                clef = clef,
                changed = changed
            };
            return meta;
        }
    }
}
