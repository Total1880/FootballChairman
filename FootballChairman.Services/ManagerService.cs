using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<Club> _clubRepository;
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Tactic> _tacticRepository;
        private readonly IPersonNameService _personNameService;
        private readonly Random random = new Random();

        public ManagerService(IRepository<Manager> managerRepository, IPersonNameService personNameService, IRepository<Club> clubRepository, IRepository<Player> playerRepository, IRepository<Tactic> tacticRepository)
        {
            _managerRepository = managerRepository;
            _personNameService = personNameService;
            _clubRepository = clubRepository;
            _playerRepository = playerRepository;
            _tacticRepository = tacticRepository;
        }
        public Manager CreateManager(Manager manager)
        {
            var list = _managerRepository.Get();
            if (!list.Any(m => m.Id == manager.Id))
            {
                list.Add(manager);
            }
            _managerRepository.Create(list);
            return manager;
        }

        public Manager GenerateManager(int clubId, int countryId)
        {
            Type managerType = typeof(ManagerType);
            Type facilityUpgradeType = typeof(FacilityUpgradeType);
            Array managerTypeValues = managerType.GetEnumValues();
            Array facilityUpgradeTypeValues = facilityUpgradeType.GetEnumValues();



            var allManagers = GetAllManagers();
            int newid;
            if (allManagers.Count > 0)
            {
                newid = allManagers.Max(m => m.Id) + 1;
            }
            else
            {
                newid = 0;
            }

            var newManager = new Manager();
            newManager.Id = newid;
            newManager.FirstName = _personNameService.GetRandomFirstName(countryId);
            newManager.LastName = _personNameService.GetRandomLastName(countryId);
            newManager.TrainingDefenseSkill = random.Next(1, 100);
            newManager.TrainingAttackSkill = random.Next(1, 100);
            newManager.TrainingMidfieldSkill = random.Next(1, 100);
            newManager.TrainingGoalkeepingSkill = random.Next(1, 100);
            newManager.Age = random.Next(40, 65);
            newManager.ClubId = clubId;
            newManager.CountryId = countryId;
            newManager.ManagerType = (ManagerType)managerTypeValues.GetValue(random.Next(managerTypeValues.Length));
            newManager.FacilityUpgradeType = (FacilityUpgradeType)facilityUpgradeTypeValues.GetValue(random.Next(facilityUpgradeTypeValues.Length));
            newManager.ContractYears = random.Next(1, 5);
            newManager.Wage = (int)_clubRepository.Get().FirstOrDefault(c => c.Id == clubId).PlayerBudget;
            CreateManager(newManager);
            return newManager;
        }

        public IList<Manager> GetAllManagers()
        {
            return _managerRepository.Get();
        }

        public Manager GetManager(int id)
        {
            return _managerRepository.Get().FirstOrDefault(m => m.Id == id);

        }

        public Manager GetManagerFromClub(int clubId)
        {
            return _managerRepository.Get().FirstOrDefault(m => m.ClubId == clubId);
        }

        public Manager UpdateManager(Manager manager)
        {
            var list = GetAllManagers().Where(m => m.Id != manager.Id).ToList();
            list.Add(manager);
            _managerRepository.Create(list);

            return manager;
        }

        public IList<Manager> UpdateManagersEndSeason()
        {
            var newManagers = new List<Manager>();
            var allManagers = GetAllManagers();
            var allManagersToLoop = new List<Manager>(allManagers);
            foreach (var manager in allManagersToLoop)
            {
                manager.Age++;
                manager.ContractYears--;
                if (manager.Age > 65)
                {
                    if (random.Next(0, 5) == 0)
                    {
                        var newManager = GenerateManager(manager.ClubId, manager.CountryId);
                        allManagers.Remove(manager);
                        allManagers.Add(newManager);
                        newManagers.Add(newManager);
                    }
                }
            }

            _managerRepository.Create(allManagers);

            return newManagers;
        }

        public IList<Transfer> DoTransfers(int year)
        {
            var clubs = _clubRepository.Get().OrderByDescending(c => c.Reputation).ToList();
            var managers = GetAllManagers();
            var tactics = _tacticRepository.Get();
            var transfers = new List<Transfer>();

            List<Player> players = _playerRepository.Get().ToList();

            FillTacticsWithPlayers(tactics, players);

            foreach (var club in clubs)
            {
                var lowerRepClubs = clubs.Where(c => c.Reputation < club.Reputation);

                if (lowerRepClubs.Count() < 1)
                    continue;

                var transferablePlayers = new List<Player>();

                foreach (var lowerRepClub in lowerRepClubs)
                {
                    transferablePlayers.AddRange(players.Where(p => 
                    p.ClubId == lowerRepClub.Id &&
                    (p.ContractYears < 1 || p.TransferValue <= club.Budget) &&
                    p.Wage < club.PlayerBudget
                    ).ToList());
                }

                var tactic = tactics.FirstOrDefault(t => t.ClubId == club.Id);
                var playersToTransfer = new List<Player>();

                switch (managers.FirstOrDefault(m => m.ClubId == club.Id).ManagerType)
                {
                    case ManagerType.Defensive:
                        playersToTransfer.Add(TransferGoalkeepingPlayer(transferablePlayers, tactic.Goalkeeper));
                        playersToTransfer.Add(TransferDefendingPlayer(transferablePlayers, tactic.Defenders));
                        playersToTransfer.Add(TransferMidfieldlayer(transferablePlayers, tactic.Midfielders));
                        playersToTransfer.Add(TransferAttackingPlayer(transferablePlayers, tactic.Attackers));
                        break;

                    case ManagerType.BalancedDefensive:
                        playersToTransfer.Add(TransferGoalkeepingPlayer(transferablePlayers, tactic.Goalkeeper));
                        playersToTransfer.Add(TransferMidfieldlayer(transferablePlayers, tactic.Midfielders));
                        playersToTransfer.Add(TransferDefendingPlayer(transferablePlayers, tactic.Defenders));
                        playersToTransfer.Add(TransferAttackingPlayer(transferablePlayers, tactic.Attackers));
                        break;

                    case ManagerType.BalancedAttacking:
                        playersToTransfer.Add(TransferGoalkeepingPlayer(transferablePlayers, tactic.Goalkeeper));
                        playersToTransfer.Add(TransferMidfieldlayer(transferablePlayers, tactic.Midfielders));
                        playersToTransfer.Add(TransferAttackingPlayer(transferablePlayers, tactic.Attackers));
                        playersToTransfer.Add(TransferDefendingPlayer(transferablePlayers, tactic.Defenders));
                        break;

                    case ManagerType.Attacking:
                        playersToTransfer.Add(TransferAttackingPlayer(transferablePlayers, tactic.Attackers));
                        playersToTransfer.Add(TransferMidfieldlayer(transferablePlayers, tactic.Midfielders));
                        playersToTransfer.Add(TransferGoalkeepingPlayer(transferablePlayers, tactic.Goalkeeper));
                        playersToTransfer.Add(TransferDefendingPlayer(transferablePlayers, tactic.Defenders));
                        break;

                    default:
                        throw new NotImplementedException("This managertype is not implemented: " + managers.FirstOrDefault(m => m.ClubId == club.Id).ManagerType.ToString());
                }
                foreach (var player in playersToTransfer)
                {
                    if (player == null)
                    {
                        continue;
                    }
                    var transfer = new Transfer();
                    transfer.Year = year;
                    transfer.Player.Id = player.Id;
                    transfer.PreviousClub.Id = player.ClubId;
                    transfer.NextClub.Id = club.Id;
                    transfer.TransferValue = player.TransferValue;
                    transfers.Add(transfer);

                    var lookupPlayer = players.FirstOrDefault(p => p.Id == player.Id);
                    lookupPlayer.ClubId = club.Id;
                    lookupPlayer.Wage = (int)club.PlayerBudget;

                    if (lookupPlayer.Age < 22)
                    {
                        lookupPlayer.ContractYears = 5;
                    }
                    else if (lookupPlayer.Age < 26)
                    {
                        lookupPlayer.ContractYears = 4;
                    }
                    else if (lookupPlayer.Age < 31)
                    {
                        lookupPlayer.ContractYears = 3;
                    }
                    else if (lookupPlayer.Age < 35)
                    {
                        lookupPlayer.ContractYears = 2;
                    }
                    else
                    {
                        lookupPlayer.ContractYears = 1;
                    }
                }
            }

            _playerRepository.Create(players);
            return transfers;
        }

        private Player TransferGoalkeepingPlayer(IList<Player> transferablePlayers, Player goalkeeper)
        {
            var potentialTransfer = transferablePlayers.OrderBy(p => p.Age).OrderByDescending(p => p.Goalkeeping).FirstOrDefault();
            if (potentialTransfer != null && potentialTransfer.Goalkeeping > goalkeeper.Goalkeeping)
            {
                return potentialTransfer;
            }

            return null;
        }
        private Player TransferDefendingPlayer(IList<Player> transferablePlayers, IList<Player> clubDefenders)
        {
            var potentialTransfer = transferablePlayers.OrderBy(p => p.Age).OrderByDescending(p => p.Defense).FirstOrDefault();
            if (potentialTransfer != null && potentialTransfer.Defense > clubDefenders.Min(p => p.Defense))
            {
                return potentialTransfer;
            }

            return null;
        }
        private Player TransferMidfieldlayer(IList<Player> transferablePlayers, IList<Player> clubMidfielders)
        {
            var potentialTransfer = transferablePlayers.OrderBy(p => p.Age).OrderByDescending(p => p.Midfield).FirstOrDefault();
            if (potentialTransfer != null && potentialTransfer.Midfield > clubMidfielders.Min(p => p.Midfield))
            {
                return potentialTransfer;
            }

            return null;
        }
        private Player TransferAttackingPlayer(IList<Player> transferablePlayers, IList<Player> clubAttackers)
        {
            var potentialTransfer = transferablePlayers.OrderBy(p => p.Age).OrderByDescending(p => p.Attack).FirstOrDefault();
            if (potentialTransfer != null && potentialTransfer.Attack > clubAttackers.Min(p => p.Attack))
            {
                return potentialTransfer;
            }

            return null;
        }
        private void FillTacticsWithPlayers(IList<Tactic> tactics, IList<Player> players)
        {
            foreach (var tactic in tactics)
            {
                var keeperToAdd = (players.FirstOrDefault(p => p.Id == tactic.GoalkeeperId));
                if (keeperToAdd == null)
                {
                    keeperToAdd = new Player { Goalkeeping = -1, Defense = -1, Midfield = -1, Attack = -1 };
                }
                tactic.Goalkeeper = players.FirstOrDefault(keeperToAdd);

                tactic.Defenders = new List<Player>();
                foreach (var d in tactic.DefendersId)
                {
                    var playerToAdd = players.FirstOrDefault(p => p.Id == d);
                    if (playerToAdd == null)
                    {
                        playerToAdd = new Player { Goalkeeping = -1, Defense = -1, Midfield = -1, Attack = -1 };
                    }
                    tactic.Defenders.Add(playerToAdd);
                }

                tactic.Midfielders = new List<Player>();
                foreach (var m in tactic.MidfieldersId)
                {
                    var playerToAdd = players.FirstOrDefault(p => p.Id == m);
                    if (playerToAdd == null)
                    {
                        playerToAdd = new Player { Goalkeeping = -1, Defense = -1, Midfield = -1, Attack = -1 };
                    }
                    tactic.Midfielders.Add(playerToAdd);
                }

                tactic.Attackers = new List<Player>();
                foreach (var a in tactic.AttackersId)
                {
                    var playerToAdd = players.FirstOrDefault(p => p.Id == a);
                    if (playerToAdd == null)
                    {
                        playerToAdd = new Player { Goalkeeping = -1, Defense = -1, Midfield = -1, Attack = -1 };
                    }
                    tactic.Attackers.Add(playerToAdd);
                }
            }
        }
    }
}
