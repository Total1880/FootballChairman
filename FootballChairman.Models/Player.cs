using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Models
{
    public class Player : Person
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Midfield { get; set; }
    }
}
