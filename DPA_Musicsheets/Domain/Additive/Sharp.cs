
namespace DPA_Musicsheets.Domain.Additive
{
    class Sharp : ICrossMole
    {
        private readonly int sharps;
        public static Sharp create(int sharps = 1)
        {
            return new Sharp(sharps);
        }

        private Sharp(int sharps)
        {
            this.sharps = sharps;
        }

        public int getModifier()
        {
            return -sharps;
        }
    }
}
