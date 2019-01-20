using System.Collections.Generic;

namespace DPA_Musicsheets.Converters
{
    public interface IConverter<T>
    {
        object convert(List<T> musicList);
    }
}
