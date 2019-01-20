using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Domain
{
    static class SymbolFactory
    {
        public static Meta getMeta()
        {
            return Meta.create();
        }

        public static Note getNote(Enums.NoteHeight noteHeight)
        {
            return Note.create(noteHeight);
        }

        public static Bar getBar()
        {
            return Bar.create();
        }

        public static Rest getRest()
        {
            return Rest.create();
        }

        public static Rest getRest(int duration)
        {
            return Rest.create(duration);
        }
    }
}
