using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Converters;
using DPA_Musicsheets.Domain;

namespace DPA_Musicsheets.Events
{
    public class MusicLoadedEventArgs : EventArgs
    {
        public List<ISymbol> symbolList;
        public IConverter<ISymbol> staffsConverter;
        public IConverter<ISymbol> editorConverter;
    }
}
