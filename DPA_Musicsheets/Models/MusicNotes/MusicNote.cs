namespace DPA_Musicsheets.Models.MusicNotes
{
    public class MusicNote : BaseNote
    {
        public PitchType Pitch { get; set; }
        public bool IsPoint { get; set; }
        public int Octave { get; set; }

        public MusicNote(PitchType pitch, bool isPoint, int octave)
        {
            Pitch = pitch;
            IsPoint = isPoint;
            Octave = octave;
        }
    }
}