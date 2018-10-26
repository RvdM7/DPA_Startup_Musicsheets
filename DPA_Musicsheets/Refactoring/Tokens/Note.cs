using DPA_Musicsheets.Refactoring.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring
{
    public class Note : INote
    {
        //public int komma { get; set; }
        //public int apostrof { get; set; }
        //public bool flat { get; set; }
        //public bool sharp { get; set; }
        //public int dot { get; set; } // of de noot een punt bezit, die maakt de note de helft langer
        //string herhalingen (advanced)

        private char height; // hoogte van een note
        private int length; // lengte van een note

        public char getHeight()
        {
            return height;
            //throw new NotImplementedException();
        }

        public int getLength()
        {
            return length;
            //throw new NotImplementedException();
        }

        public void setHeight(char height)
        {
            this.height = height;
            //throw new NotImplementedException();
        }

        public void setLength(int length)
        {
            this.length = length;
            //throw new NotImplementedException();
        }
    }
}
