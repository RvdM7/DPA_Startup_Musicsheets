using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Tokens.NoteDecorators
{
    class FlatSharpDecorator : NoteDecorator
    {
        public FlatSharpDecorator(INote token) : base(token)
        {

        }
        public override char getHeight()
        {
            return base.getHeight();
        }

        public override int getLength()
        {
            return base.getLength();
        }

        public override void setHeight(char height)
        {
            base.setHeight(height);
        }

        public override void setLength(int length)
        {
            base.setLength(length);
        }
    }
}
