﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Refactoring.Domain.Additive
{
    interface IOctaveModifier
    {
        int getModifier();
        void addModifier(int additive);
    }
}
