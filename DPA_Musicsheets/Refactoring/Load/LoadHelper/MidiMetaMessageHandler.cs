using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Refactoring.Domain;
using Sanford.Multimedia.Midi;

namespace DPA_Musicsheets.Refactoring.Load.LoadHelper
{
    class MidiMetaMessageHandler : IMidiMessageHandler
    {
        public void handleMessage(MidiEvent midiEvent, ref LoadMidi.LoadVars vars, ref Note addNote, ref List<ISymbol> symbols)
        {
            var metaMessage = midiEvent.MidiMessage as MetaMessage;

            switch (metaMessage.MetaType)
            {
                case MetaType.TimeSignature:
                    byte[] timeSignatureBytes = metaMessage.GetBytes();
                    vars.meta.beatNote = timeSignatureBytes[0];
                    vars.meta.beatsPerBar = (int)(1 / Math.Pow(timeSignatureBytes[1], -2));
                    vars.meta.changed = true;
                    break;
                case MetaType.Tempo:
                    byte[] tempoBytes = metaMessage.GetBytes();
                    int tempo = (tempoBytes[0] & 0xff) << 16 | (tempoBytes[1] & 0xff) << 8 | (tempoBytes[2] & 0xff);
                    vars.meta.bpm = 60000000 / tempo;
                    vars.meta.changed = true;
                    break;
                case MetaType.EndOfTrack:

                    if (vars.previousNoteAbsoluteTicks > 0)
                    {
                        // Finish the last notelength.
                        double percentageOfBar;
                        LoadMidi.midiHelper.setNoteLength(vars.previousNoteAbsoluteTicks, midiEvent.AbsoluteTicks, vars.division, vars.meta.beatNote, vars.meta.beatsPerBar, out percentageOfBar, addNote);

                        vars.percentageOfBarReached += percentageOfBar;
                        if (vars.percentageOfBarReached >= 1)
                        {
                            percentageOfBar = percentageOfBar - 1;
                            symbols.Add(new Bar());
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}
