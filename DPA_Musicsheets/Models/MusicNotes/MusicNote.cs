namespace DPA_Musicsheets.Models.MusicNotes
{
    public class MusicNote : BaseNote
    {
        public PitchType PitchType { get; set; }
        public PitchModifier PitchModifier { get; set; }
        public bool IsPoint { get; set; }
        public int Octave { get; set; }

        public MusicNote(NoteDuration duration, PitchType pitchType, PitchModifier pitchModifier, bool isPoint, int octave) 
            : base(duration)
        {
            PitchType = pitchType;
            PitchModifier = pitchModifier;
            IsPoint = isPoint;
            Octave = octave;
        }
    }
}