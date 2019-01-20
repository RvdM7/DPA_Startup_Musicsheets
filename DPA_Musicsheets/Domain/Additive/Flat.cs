
namespace DPA_Musicsheets.Domain.Additive
{
    class Flat : ICrossMole
    {
        private readonly int flats;

        public static Flat create(int flats = 1)
        {
            return new Flat(flats);
        }

        private Flat(int flats)
        {
            this.flats = flats;
        }

        public int getModifier()
        {
            return flats;
        }
    }
}
