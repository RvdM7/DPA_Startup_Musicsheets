using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Models.Commandos;
using System.Linq;
using System.Text;
using System;
using DPA_Musicsheets.Refactoring.Domain;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLilypond : ILoader
    {
        private readonly string fileName;

        public LoadLilypond(string fileName)
        {
            this.fileName = fileName;
        }
        public List<ISymbol> loadMusic()
        {
            throw new NotImplementedException();
            LinkedList<IMusicToken> ll = new LinkedList<IMusicToken>();

            StringBuilder sb = new StringBuilder();
            foreach (var line in File.ReadAllLines(fileName))
            {
                sb.AppendLine(line);
            }
            string content = sb.ToString();

            foreach (string s in content.Split(' ').Where(item => item.Length > 0))
            {
                switch (s)
                {
                    case "\\relative": ll.AddLast(new Relative()); break;
                    case "\\clef": ll.AddLast(new Clef()); break;
                    case "\\time": ll.AddLast(new Time()); break;
                    case "\\tempo": ll.AddLast(new Tempo()); break;
                    case "\\repeat": ll.AddLast(new Repeat()); break;
                    case "\\alternative": ll.AddLast(new Alternative()); break;
                    case "{": ll.AddLast(new SectionStart()); break;
                    case "}": ll.AddLast(new SectionEnd()); break;
                    //case "|": ll.AddLast(new Domain.Bar()); break;
                    default:
                        if (new Regex(@"[~]?[a-g][,'eis]*[0-9]+[.]*").IsMatch(s))
                        {
                            //ll.AddLast(new BaseNote())
                        }
                        break;
                }

            }
            //return ll;
        }
    }
}
