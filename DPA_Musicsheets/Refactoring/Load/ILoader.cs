using DPA_Musicsheets.Models;
using DPA_Musicsheets.Refactoring.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load
{
    abstract class ILoader
    {
        public string fileName;
        public abstract List<ISymbol> loadMusic();
    }
}
