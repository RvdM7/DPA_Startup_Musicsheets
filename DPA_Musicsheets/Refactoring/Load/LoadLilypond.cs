using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models.MusicNotes;

namespace DPA_Musicsheets.Refactoring.Load
{
    class LoadLilypond : IMusicLoader
    {
        LinkedList<BaseNote> IMusicLoader.loadMusic(string fileName)
        {
            //throw new NotImplementedException();
            LinkedList<BaseNote> ll = new LinkedList<BaseNote>();

            foreach (var line in File.ReadAllLines(fileName))
            {
                /* TODO Werk uit
                switch (line)
                {
                    case "\\relative": token.TokenKind = LilypondTokenKind.Staff; break;
                    case "\\clef": token.TokenKind = LilypondTokenKind.Clef; break;
                    case "\\time": token.TokenKind = LilypondTokenKind.Time; break;
                    case "\\tempo": token.TokenKind = LilypondTokenKind.Tempo; break;
                    case "\\repeat": token.TokenKind = LilypondTokenKind.Repeat; break;
                    case "\\alternative": token.TokenKind = LilypondTokenKind.Alternative; break;
                    case "{": token.TokenKind = LilypondTokenKind.SectionStart; break;
                    case "}": token.TokenKind = LilypondTokenKind.SectionEnd; break;
                    case "|": token.TokenKind = LilypondTokenKind.Bar; break;
                    default: token.TokenKind = LilypondTokenKind.Unknown; break;
                }
                */
            }

            return ll;
        }
    }
}
