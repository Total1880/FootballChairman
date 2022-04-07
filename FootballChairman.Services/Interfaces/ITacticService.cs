using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface ITacticService
    {
        public Tactic GetStandardTactic(int clubId);
        public Tactic CreateTactic(Tactic tactic);
        public Tactic GetTactic(int clubId);
    }
}
