
namespace DPA_Musicsheets.Domain.Clefs
{
    class Treble : IClef
    {
        public static Treble create()
        {
            return new Treble();
        }

        private Treble() { }

        public override string ToString()
        {
            return "Treble";
        }
    }
}
