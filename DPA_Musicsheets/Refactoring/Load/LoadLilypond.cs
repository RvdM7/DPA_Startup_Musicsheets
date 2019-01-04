using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond;
using DPA_Musicsheets.Refactoring.Domain.Enums;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLilypond : ILoader
    {
        private Dictionary<string, ILilypondMessageHandler> strategies = new Dictionary<string, ILilypondMessageHandler>();

        public struct LoadVars
        {
            public Meta meta;
            public int previousOctave;
            public char previousNoteHeight;
        }

        public LoadLilypond()
        {
            // Add strategies
            strategies.Add("\\relative", new LilypondStaffHandler());
            strategies.Add("\\clef", new LilypondClefHandler());
            strategies.Add("\\time", new LilypondTimeHandler());
            strategies.Add("\\tempo", new LilypondTempoHandler());
            strategies.Add("\\repeat", new LilypondRepeatHandler());
            strategies.Add("\\alternative", new LilypondAlternativeHandler());
            strategies.Add("{", new LilypondSectionStartHandler());
            strategies.Add("}", new LilypondSectionEndHandler());
            strategies.Add("|", new LilypondBarHandler());
            strategies.Add("Note", new LilypondNoteHandler());
            strategies.Add("~", new LilypondBridgeHandler());
        }

        public override List<ISymbol> loadMusic()
        {
            LoadVars vars = new LoadVars
            {
                meta = new Meta(),
                previousOctave = 4,
                previousNoteHeight = (char)NoteHeight.c
            };

            List<ISymbol> symbols = new List<ISymbol>();
            ISymbol addSymbol = null;
            ILilypondMessageHandler currentStrategy = null;
            string content = readFile();

            foreach (string s in content.Split(' ').Where(item => item.Length > 0))
            {
                symbols.Add(addSymbol);
                if (currentStrategy != null)
                {
                    currentStrategy.handleMessage(s, ref vars, ref addSymbol);
                }
                if (!checkRegex(s, ref vars, ref addSymbol) && strategies.ContainsKey(s))
                {
                    currentStrategy = strategies[s];
                    currentStrategy.handleMessage(s, ref vars, ref addSymbol);
                    continue;
                }
                else
                {
                    currentStrategy = null;
                }
            }

            symbols.RemoveAll(item => item == null);
            return symbols;
        }

        private bool checkRegex(string s, ref LoadVars vars, ref ISymbol addSymbol)
        {
            if (new Regex(@"[~]?[a-g][,'eis]*[0-9]+[.]*").IsMatch(s))
            {
                // note
                strategies["Note"].handleMessage(s, ref vars, ref addSymbol);
                return true;
            }
            else if (new Regex(@"r.*?[0-9][.]*").IsMatch(s))
            {
                // rest
                return true;
            }
            return false;
        }

        private string readFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in File.ReadAllLines(fileName))
            {
                sb.AppendLine(line);
            }
            string content = sb.ToString().Replace(Environment.NewLine, " ");

            return content;
        }
    }
}
