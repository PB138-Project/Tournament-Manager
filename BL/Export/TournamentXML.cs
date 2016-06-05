using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Export
{
    public class TournamentXML
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public int TournamentSize { get; set; }
        public List<string> Matches { get; set; }
    }
}
