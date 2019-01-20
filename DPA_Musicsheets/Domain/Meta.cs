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

        void setChanged(bool setter)
        {
            changed = setter;
        }

        public bool isReady() => bpm != 0 && beatNote != 0 && beatsPerBar != 0 && clef != null;

        public override string ToString()
        {
            return $"{clef}: {bpm}-{beatNote}-{beatsPerBar}";
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
