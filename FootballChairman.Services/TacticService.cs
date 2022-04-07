using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class TacticService : ITacticService
    {
        private readonly IPlayerService _playerService;
        private readonly IManagerService _managerService;
        private readonly IRepository<Tactic> _tacticRepository;

        public TacticService(IPlayerService playerService, IManagerService managerService, IRepository<Tactic> tacticRepository)
        {
            _playerService = playerService;
            _managerService = managerService;
            _tacticRepository = tacticRepository;
        }

        public Tactic GetStandardTactic(int clubId)
        {
            var players = _playerService.GetPlayersFromClub(clubId).OrderByDescending(p => p.Goalkeeping).ToList();
            var manager = _managerService.GetManagerFromClub(clubId);

            switch (manager.ManagerType)
            {
                case ManagerType.Defensive:
                    return CreateTactic(GetDefensiveTactic(clubId, players));
                case ManagerType.BalancedDefensive:
                    return CreateTactic(GetBalancedDefensiveTactic(clubId, players));
                case ManagerType.BalancedAttacking:
                    return CreateTactic(GetBalacedAttackingTactic(clubId, players));
                case ManagerType.Attacking:
                    return CreateTactic(GetAttackingTactic(clubId, players));
                default:
                    throw new NotImplementedException("This managertype is not implemented: " + manager.ManagerType.ToString());
            }

        }

        private Tactic GetDefensiveTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            players = players.OrderByDescending(p => p.Goalkeeping).ToList();
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
            players = players.OrderByDescending(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            return tactic;
        }

        private Tactic GetBalancedDefensiveTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            players = players.OrderByDescending(p => p.Goalkeeping).ToList();
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
            players = players.OrderByDescending(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            return tactic;
        }

        private Tactic GetBalacedAttackingTactic(int clubId, List<Player> players)
        {
            var tactic = new Tactic { ClubId = clubId };

            //Get Goalkeeper
            players = players.OrderByDescending(p => p.Goalkeeping).ToList();
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get 2 Attackers
            tactic.Attackers = new List<Player>();
            players = players.OrderByDescending(p => p.Attack).ToList();
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
            players = players.OrderByDescending(p => p.Attack).ToList();
            tactic.Attackers = players.GetRange(0, 2);
            players.RemoveRange(0, 2);

            //Get 4 Midfielders
            tactic.Midfielders = new List<Player>();
            players = players.OrderByDescending(p => p.Midfield).ToList();
            tactic.Midfielders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            //Get Goalkeeper
            players = players.OrderByDescending(p => p.Goalkeeping).ToList();
            tactic.Goalkeeper = players.FirstOrDefault();
            players.Remove(tactic.Goalkeeper);

            //Get 4 defenders
            tactic.Defenders = new List<Player>();
            players = players.OrderByDescending(p => p.Defense).ToList();
            tactic.Defenders = players.GetRange(0, 4);
            players.RemoveRange(0, 4);

            return tactic;
        }

        public Tactic CreateTactic(Tactic tactic)
        {
            var allTactics = _tacticRepository.Get().Where(t => t.ClubId != tactic.ClubId).ToList();
            tactic.GoalkeeperId = tactic.Goalkeeper.Id;

            tactic.DefendersId = new List<int>();
            foreach (var d in tactic.Defenders)
            {
                tactic.DefendersId.Add(d.Id);
            }

            tactic.MidfieldersId = new List<int>();
            foreach (var m in tactic.Midfielders)
            {
                tactic.MidfieldersId.Add(m.Id);
            }

            tactic.AttackersId = new List<int>();
            foreach (var a in tactic.Attackers)
            {
                tactic.AttackersId.Add(a.Id);
            }

            allTactics.Add(tactic);
            _tacticRepository.Create(allTactics);
            return tactic;
        }

        public Tactic GetTactic(int clubId)
        {
            var players = _playerService.GetPlayersFromClub(clubId);

            var tactic = _tacticRepository.Get().FirstOrDefault(t => t.ClubId == clubId);

            tactic.Goalkeeper = players.FirstOrDefault(p => p.Id == tactic.GoalkeeperId);

            tactic.Defenders = new List<Player>();
            foreach (var d in tactic.DefendersId)
            {
                tactic.Defenders.Add(players.FirstOrDefault(p => p.Id == d));
            }

            tactic.Midfielders = new List<Player>();
            foreach (var m in tactic.MidfieldersId)
            {
                tactic.Midfielders.Add(players.FirstOrDefault(p => p.Id == m));
            }

            tactic.Attackers = new List<Player>();
            foreach (var a in tactic.AttackersId)
            {
                tactic.Attackers.Add(players.FirstOrDefault(p => p.Id == a));
            }

            return tactic;
        }
    }
}
