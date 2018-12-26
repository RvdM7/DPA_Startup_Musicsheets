using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper
{
    interface IMidiMessageHandler
    {
        void handleMessage(IMidiMessage midiMessage, Meta meta);
    }
}
