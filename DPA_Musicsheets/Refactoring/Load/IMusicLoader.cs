using DPA_Musicsheets.Models.MusicNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load
{
    interface IMusicLoader
    {
        LinkedList<BaseNote> loadMusic(string content);
    }
}
