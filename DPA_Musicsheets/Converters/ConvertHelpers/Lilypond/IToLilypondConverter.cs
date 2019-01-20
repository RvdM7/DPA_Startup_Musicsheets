using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Lilypond
{
    interface IToLilypondConverter
    {
        string convert(ISymbol symbol);
    }
}
