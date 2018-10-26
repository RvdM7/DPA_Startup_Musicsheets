namespace DPA_Musicsheets.Models.MusicNotes
{
    public abstract class BaseNote : IMusicToken
    {
        public NoteDuration Duration { get; set; }

        public BaseNote(NoteDuration duration)
        {
            Duration = duration;
        }
    }
}