using DPA_Musicsheets.Models.MusicNotes;
using DPA_Musicsheets.Refactoring.Load;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring
{
    class MusicLoader
    {
        LoadLocator ll = new LoadLocator();

        public void loadMusic(String fileName)
        {
            IMusicLoader ml = ll.LocateLoader(fileName);
            LinkedList<BaseNote> lList = ml.loadMusic(fileName);
        }
    }
}
