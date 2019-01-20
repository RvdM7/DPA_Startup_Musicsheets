
namespace DPA_Musicsheets.Domain.Additive
{
    class Sharp : ICrossMole
    {
        private readonly int sharps;

        public Sharp() { sharps = 1; }

        public Sharp(int sharps)
        {
            this.sharps = sharps;
        }

        public int getModifier()
        {
            //throw new NotImplementedException();
            return -sharps;
        }
    }
}
