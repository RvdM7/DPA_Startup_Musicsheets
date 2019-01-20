using DPA_Musicsheets.Domain.Clefs;

namespace DPA_Musicsheets.Domain
{
    class Meta : ISymbol
    {
        public int bpm = 0;
        public int beatNote = 0;
        public int beatsPerBar = 0;
        public bool changed = true;
        public IClef clef = null;

        public static Meta create()
        {
            return new Meta();
        }

        private Meta() { }

        void setChanged(bool setter)
        {
            changed = setter;
        }

        public bool isReady() => bpm != 0 && beatNote != 0 && beatsPerBar != 0 && clef != null;

        public override string ToString()
        {
            return $"{clef}: {bpm}-{beatNote}-{beatsPerBar}";
        }

        public bool compare(Meta other)
        {
            if (bpm == other.bpm
                    && beatNote == other.beatNote
                    && beatsPerBar == other.beatsPerBar
                    && clef == other.clef)
            {
                return true;
            }
            return false;
        }

        public Meta clone()
        {
            Meta meta = SymbolFactory.getMeta();
            meta.bpm = bpm;
            meta.beatNote = beatNote;
            meta.beatsPerBar = beatsPerBar;
            meta.clef = clef;
            meta.changed = changed;

            return meta;
        }
    }
}
