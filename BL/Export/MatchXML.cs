using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Export
{
    public class MatchXML
    {
        public int Id { get; set; }
        public int TeamAId { get; set; }
        public string TeamA { get; set; }
        public int TeamBId { get; set; }
        public string TeamB { get; set; }
        public int? WinnerId { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"Match number {Id}." + Environment.NewLine
                + $"{TeamA} vs {TeamB}." + Environment.NewLine);
            if (WinnerId == null)
            {
                result.Append($"Match not finished yet.");
            } else
            {
                result.Append($"Winner is ");
                if (WinnerId == TeamAId)
                {
                    result.Append($"{TeamA}.");
                } else
                {
                    result.Append($"{TeamB}.");
                }
            }
            return result.ToString() + Environment.NewLine;
        }
    }
}
