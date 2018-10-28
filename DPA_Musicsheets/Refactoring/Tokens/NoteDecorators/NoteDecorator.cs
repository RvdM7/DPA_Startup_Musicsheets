using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Tokens.NoteDecorators
{
    public abstract class NoteDecorator : INote
    {
        private INote token;

        public NoteDecorator(INote token)
        {
            this.token = token;
        }

        public virtual char getHeight()
        {
            return token.getHeight();
        }

        public virtual int getLength()
        {
            return token.getLength();
        }

        public virtual void setHeight(char height)
        {
            token.setHeight(height);
        }

        public virtual void setLength(int length)
        {
            token.setLength(length);
        }
    }
}
