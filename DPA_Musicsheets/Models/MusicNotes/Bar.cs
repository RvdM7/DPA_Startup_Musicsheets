using System.Collections.Generic;
using System.Windows.Documents;

namespace DPA_Musicsheets.Models.MusicNotes
{
    public class Bar
    {
        public List<BaseNote> Notes { get; set; } = new List<BaseNote>();
    }
}