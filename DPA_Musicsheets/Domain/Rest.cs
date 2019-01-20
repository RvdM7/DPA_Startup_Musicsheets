
namespace DPA_Musicsheets.Domain
{
    class Rest : ISymbol
    {
        public int duration;

        public Rest() { }

        public Rest(int duration)
        {
            this.duration = duration;
        }

        public override string ToString()
        {
            return $"'r{duration}'";
        }
    }
}
