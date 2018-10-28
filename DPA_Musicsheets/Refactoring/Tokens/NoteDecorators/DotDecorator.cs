using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Tokens.NoteDecorators
{
    class DotDecorator : NoteDecorator
    {
        private int dots;

        public DotDecorator(INote token, int dots) : base(token)
        {
            this.dots = dots;
        }

        public override char getHeight()
        {
            return base.getHeight();
        }

        public override int getLength()
        {
            // TODO process dots
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
