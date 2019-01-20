using System.Collections.Generic;
using DPA_Musicsheets.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Load.LoadHelper.Midi
{
    interface IMidiMessageHandler
    {
        void handleMessage(MidiEvent midiEvent, ref LoadMidi.LoadVars vars, ref ISymbol addNote, ref List<ISymbol> symbols);
    }
}
