using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BL.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public List<PlayerDTO> Players { get; set; }

        public override string ToString()
        {
            return $"ID = {Id} \nName = {TeamName} \n" + Players.ToString();
        }
        //ToString of List<PlayerDTO> Players is not done, since the ToString method is here only to prove that 
        //the method GetSpecificTeam in SimpleConsoleApp/Program.cs is working correctly.
    }
}