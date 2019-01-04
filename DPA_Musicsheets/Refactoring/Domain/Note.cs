﻿using DPA_Musicsheets.Refactoring.Domain.Additive;
using DPA_Musicsheets.Refactoring.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain
{
    class Note : ISymbol
    {
        public Dots dots = null;
        public ICrossMole crossMole = null;
        public int octave = 0;
        public int duration = 0;
        public readonly NoteHeight height;


        public Note(NoteHeight noteHeight)
        {
            height = noteHeight;
        }

        public override string ToString()
        {
            return $"'{height}{duration} {octave}'";
        }
    }
}
