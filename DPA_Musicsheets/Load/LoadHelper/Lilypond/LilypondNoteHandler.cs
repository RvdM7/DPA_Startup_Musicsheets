using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DPA_Musicsheets.Domain;
using DPA_Musicsheets.Domain.Additive;
using DPA_Musicsheets.Domain.Enums;

namespace DPA_Musicsheets.Load.LoadHelper.Lilypond
{
    class LilypondNoteHandler : ILilypondMessageHandler
    {
        public void handleMessage(string value, ref LoadLilypond.LoadVars vars, ref ISymbol addSymbol)
        {
            List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };

            NoteHeight noteHeight = (NoteHeight)value[0];
            ICrossMole crossMole = null;
            int dots = Regex.Matches(value, @"\.").Count;
            int duration = int.Parse(Regex.Match(value, @"\d+").Value);

            int octaveModifier = 0;
            octaveModifier += Regex.Matches(value, @"\'").Count;
            octaveModifier -= Regex.Matches(value, @",").Count;

            int crossMoleCount = 0;
            crossMoleCount += Regex.Matches(value, @"is").Count;
            crossMoleCount -= Regex.Matches(value, @"es|as").Count;
            if (crossMoleCount != 0)
            {
                crossMole = crossMoleCount < 0 ? (ICrossMole)AdditiveFactory.getSharp(Math.Abs(crossMoleCount)) : AdditiveFactory.getFlat(crossMoleCount);
            }

            int distanceWithPreviousNote = notesorder.IndexOf((char)noteHeight) - notesorder.IndexOf(vars.previousNoteHeight);
            if (distanceWithPreviousNote > 3) // Shorter path possible the other way around
            {
                distanceWithPreviousNote -= 7; // The number of notes in an octave
            }
            else if (distanceWithPreviousNote < -3)
            {
                distanceWithPreviousNote += 7; // The number of notes in an octave
            }

            if (distanceWithPreviousNote + notesorder.IndexOf(vars.previousNoteHeight) >= 7)
            {
                vars.previousOctave++;
            }
            else if (distanceWithPreviousNote + notesorder.IndexOf(vars.previousNoteHeight) < 0)
            {
                vars.previousOctave--;
            }

            Note note = SymbolFactory.getNote(noteHeight);
            note.dots = dots > 0 ? AdditiveFactory.getDots(dots) : null;
            note.duration = duration;
            note.crossMole = crossMole;
            note.Octave = vars.previousOctave;
            note.octaveModifier = octaveModifier;


            vars.previousOctave += octaveModifier;
            vars.previousNoteHeight = (char)note.height;

            addSymbol = note;
        }
    }
}
