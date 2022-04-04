using FootballChairman.Models;
using FootballChairman.Models.Enums;
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
        private readonly IManagerService _managerService;

        public TacticService(IPlayerService playerService, IManagerService managerService)
        {
            _playerService = playerService;
            _managerService = managerService;
        }

        public Tactic GetStandardTactic(int clubId)
        {
            var players = _playerService.GetPlayersFromClub(clubId).OrderByDescending(p => p.Goalkeeping).ToList();
            var manager = _managerService.GetManagerFromClub(clubId);

            switch (manager.ManagerType)
            {
                case ManagerType.Defensive:
                    return GetDefensiveTactic(clubId, players);
                case ManagerType.BalancedDefensive:
                    return GetBalancedDefensiveTactic(clubId, players);
                case ManagerType.BalancedAttacking:
                    return GetBalacedAttackingTactic(clubId, players);
                case ManagerType.Attacking:
                    return GetAttackingTactic(clubId, players);
                default:
                    throw new NotImplementedException("This managertype is not implemented: " + manager.ManagerType.ToString());
            }

        }

        private Tactic GetDefensiveTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderBy(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            return tactic;
        }

        private Tactic GetBalancedDefensiveTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderBy(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            return tactic;
        }

        private Tactic GetBalacedAttackingTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderBy(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            return tactic;
        }

        private Tactic GetAttackingTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderBy(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get Goalkeeper
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            return tactic;
        }
    }
}
