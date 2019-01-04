using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper.Midi
{
    interface IMidiMessageHandler
    {
        void handleMessage(MidiEvent midiEvent, ref LoadMidi.LoadVars vars, ref ISymbol addNote, ref List<ISymbol> symbols);
    }
}
