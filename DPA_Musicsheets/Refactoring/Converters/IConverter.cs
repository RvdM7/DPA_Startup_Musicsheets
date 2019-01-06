using DPA_Musicsheets.Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Converters
{
    public interface IConverter<T>
    {
        object convert(List<T> musicList);
    }
}
