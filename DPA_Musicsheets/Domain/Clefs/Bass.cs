
namespace DPA_Musicsheets.Domain.Clefs
{
    class Bass : IClef
    {
        public static Bass create()
        {
            return new Bass();
        }

        private Bass() { }

        public override string ToString()
        {
            return "Bass";
        }
    }
}
