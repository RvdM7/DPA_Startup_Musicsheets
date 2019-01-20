using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Converters;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Events
{
    public class MusicLoadedEventArgs : EventArgs
    {
        public List<ISymbol> symbolList;
        public IConverter<ISymbol> staffsConverter;
        public IConverter<ISymbol> editorConverter;
    }
}
