using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Tokens
{
    public interface INote : IToken
    {
        int getLength();
        char getHeight();
        void setLength(int length);
        void setHeight(char height);
    }
}
