using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Helpers.MusicLoaderHelper
{
    class SaveToLilypond
    {
        static public void SaveToLilypondF(string fileName, string LilypondText)
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.Write(LilypondText);
                outputFile.Close();
            }
        }
    }
}
