using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Lilypond
{
    class RestToLilypond : IToLilypondConverter
    {
        public string convert(ISymbol symbol)
        {
            var rest = symbol as Rest;
            return "r" + rest.duration;
        }
    }
}
