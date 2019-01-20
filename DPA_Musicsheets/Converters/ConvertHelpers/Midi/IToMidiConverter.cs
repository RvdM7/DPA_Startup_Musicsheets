using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Converters.ConvertHelpers.Midi
{
    interface IToMidiConverter
    {
        void convert(ISymbol symbol, ref ConvertToMidi.ConvertVars convertVars);
    }
}
