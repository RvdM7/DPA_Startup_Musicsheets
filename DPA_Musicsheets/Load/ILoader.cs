using DPA_Musicsheets.Domain;
using System.Collections.Generic;

namespace DPA_Musicsheets.Load
{
    abstract class ILoader
    {
        public string file;
        public abstract List<ISymbol> loadFromFile();
    }
}
