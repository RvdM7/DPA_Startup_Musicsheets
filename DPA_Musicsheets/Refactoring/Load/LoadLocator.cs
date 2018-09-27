using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLocator
    {
        public IMusicLoader LocateLoader(string fileName)
        {
            if (Path.GetExtension(fileName).EndsWith(".mid"))
            {
                //handle midi file
                throw new NotImplementedException();

            }
            else if (Path.GetExtension(fileName).EndsWith(".ly"))
            {
                //handle lilypond file
                //throw new NotImplementedException();
                return new LoadLilypond();
            }
            else
            {
                //handle unknown
                throw new NotSupportedException($"File extension  {Path.GetExtension(fileName)} is not supproted.");
            }
        }
    }
}
