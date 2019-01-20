
namespace DPA_Musicsheets.Domain
{
    class Bar : ISymbol
    {
        public static Bar create()
        {
            return new Bar();
        }

        private Bar() { }

        public override string ToString()
        {
            return "|";
        }
    }
}
