using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System;
using DPA_Musicsheets.Refactoring.Domain;
using DPA_Musicsheets.Refactoring.Load.LoadHelper.Lilypond;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLilypond : ILoader
    {
        private readonly string fileName;
        private Dictionary<string, ILilypondMessageHandler> strategies = new Dictionary<string, ILilypondMessageHandler>();

        public struct LoadVars
        {
            public Meta meta;
        }

        public LoadLilypond(string fileName)
        {
            this.fileName = fileName;

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
        }
        public List<ISymbol> loadMusic()
        {
            //throw new NotImplementedException();
            List<ISymbol> symbols = new List<ISymbol>();
            LoadVars vars = new LoadVars
            {
                meta = new Meta()
            };
            ILilypondMessageHandler currentStrategy = null;
            ISymbol addSymbol = null;

            StringBuilder sb = new StringBuilder();
            foreach (var line in File.ReadAllLines(fileName))
            {
                sb.AppendLine(line);
            }
            string content = sb.ToString().Replace(Environment.NewLine, " ");

            foreach (string s in content.Split(' ').Where(item => item.Length > 0))
            {
                if (addSymbol != null)
                {
                    symbols.Add(addSymbol);
                    addSymbol = null;
                }
                try
                {
                    if (new Regex(@"[~]?[a-g][,'eis]*[0-9]+[.]*").IsMatch(s))
                    {
                        // note
                        strategies["Note"].handleMessage(s, ref vars, ref addSymbol);
                    }
                    else if (new Regex(@"r.*?[0-9][.]*").IsMatch(s))
                    {
                        // rest
                    }
                    else
                    {
                        currentStrategy = strategies[s];
                        currentStrategy.handleMessage(s, ref vars, ref addSymbol);
                        continue;
                    }
                    currentStrategy = null;
                }
                catch (Exception)
                {
                    currentStrategy.handleMessage(s, ref vars, ref addSymbol);
                    //System.Diagnostics.Debug.WriteLine($"Exception: >{s}<");
                }
            }
            return symbols;
        }
    }
}
