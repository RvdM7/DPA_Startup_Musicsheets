
namespace DPA_Musicsheets.Domain.Additive
{
    class Dots : IAdditive
    {
        public readonly int dots = 0;

        public static Dots create(int dots)
        {
            return new Dots(dots);
        }

        private Dots(int dots)
        {
            this.dots = dots;
        }
    }
}
