
namespace DPA_Musicsheets.Domain
{
    class Rest : ISymbol
    {
        public int duration;

        public static Rest create(int duration = 0)
        {
            return new Rest(duration);
        }

        private Rest(int duration)
        {
            this.duration = duration;
        }

        public override string ToString()
        {
            return $"'r{duration}'";
        }
    }
}
