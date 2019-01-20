using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System;
using DPA_Musicsheets.Domain;
using DPA_Musicsheets.Load.LoadHelper.Lilypond;
using DPA_Musicsheets.Domain.Enums;

namespace DPA_Musicsheets.Load
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
            strategies.Add("r", new LilypondRestHandler());
        }

        public override List<ISymbol> loadFromFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in File.ReadAllLines(file))
            {
                sb.AppendLine(line);
            }
            string content = sb.ToString();

            return loadFromString(content);
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
                strategies["r"].handleMessage(s, ref vars, ref addSymbol);
                return true;
            }
            return false;
        }

        public List<ISymbol> loadFromString(string content)
        {
            content = content.Replace(Environment.NewLine, " ");
            List<ISymbol> symbols = new List<ISymbol>();
            ISymbol addSymbol = null;
            ILilypondMessageHandler currentStrategy = null;
            LoadVars vars = new LoadVars
            {
                meta = SymbolFactory.getMeta(),
                previousOctave = 4,
                previousNoteHeight = (char)NoteHeight.c
            };

            foreach (string s in content.Split(' ').Where(item => item.Length > 0))
            {
                symbols.Add(addSymbol);
                addSymbol = null;
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
            symbols.Add(addSymbol);
            return symbols;
        }
    }
}
