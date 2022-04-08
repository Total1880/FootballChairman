using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Transfer
    {
        public int Year { get; set; }
        public Player Player { get; set; }
        public Club PreviousClub { get; set; }
        public Club NextClub { get; set; }

        public Transfer()
        {
            Player = new Player();
            PreviousClub = new Club();
            NextClub = new Club();
        }

        public string GetTranferInString
        {
            get {
                if (Player == null || PreviousClub == null || NextClub == null)
                {
                    return "empty transfer";
                }
                return Year + ": " + Player.LastName + " " + Player.FirstName + " transfered from " + PreviousClub.Name + " to " + NextClub.Name + ".";
            } 
        }
    }
}
