using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load
{
    abstract class ILoader
    {
        public string file;
        public abstract List<ISymbol> loadFromFile();
    }
}
