using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class TacticService : ITacticService
    {
        private readonly IPlayerService _playerService;

        public TacticService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Tactic GetStandardTactic(int clubId)
        {
            var tactic = new Tactic { ClubId = clubId };
            var players = _playerService.GetPlayersFromClub(clubId).OrderByDescending(p => p.Goalkeeping).ToList();

            //Get Goalkeeper
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0,4);
            players.RemoveRange(0,4);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderBy(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);

            return tactic;
        }
    }
}
