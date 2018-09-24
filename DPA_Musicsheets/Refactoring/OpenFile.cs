using DPA_Musicsheets.Refactoring.Load;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring
{
    class OpenFile
    {
        public void openFile(string fileName)
        {
            if (Path.GetExtension(fileName).EndsWith(".mid"))
            {
                //handle midi file

                throw new NotImplementedException();

                LoadMidi lm = new LoadMidi();
                lm.loadMidi();
            }
            else if (Path.GetExtension(fileName).EndsWith(".ly"))
            {
                //handle lilypond file

                throw new NotImplementedException();

                LoadLilypond ll = new LoadLilypond();
                ll.loadLilypond();
            }
            else
            {
                //handle unknown
                throw new NotSupportedException($"File extension  {Path.GetExtension(fileName)} is not supproted.");
            }
        }
    }
}
