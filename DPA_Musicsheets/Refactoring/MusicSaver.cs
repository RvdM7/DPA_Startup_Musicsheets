using DPA_Musicsheets.Refactoring.Converters;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring
{
    public class MusicSaver
    {
        MusicList musicList;

        public MusicSaver(MusicList musicList)
        {
            this.musicList = musicList;
        }

        private void convert(IConverter<ISymbol> converter)
        {
            var converted = converter.convert(musicList.Music);
        }

        public void saveToMidi()
        {
            IConverter<ISymbol> converter = new ConvertToMidi();
            convert(converter);
        }

        public void saveToLilypond()
        {
            IConverter<ISymbol> converter = new ConvertToMidi();
            convert(converter);
        }

        public void saveToPDF()
        {
            IConverter<ISymbol> converter = new ConvertToMidi();
            convert(converter);
        }
    }
}
