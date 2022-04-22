using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly Random random = new Random();
        private readonly IRepository<Player> _playerRepository;
        private readonly IPersonNameService _personNameService;
        private readonly ICountryService _countryService;
        private readonly IClubService _clubService;
        private readonly IManagerService _managerService;

        public PlayerService(
            IRepository<Player> playerRepository,
            IPersonNameService personNameService,
            ICountryService countryService,
            IClubService clubService,
            IManagerService managerService)
        {
            _playerRepository = playerRepository;
            _personNameService = personNameService;
            _countryService = countryService;
            _clubService = clubService;
            _managerService = managerService;
        }

        public Player GenerateRandomPlayer(int clubId, int countryId)
        {
            var allPlayers = _playerRepository.Get();
            var newId = 0;

            if (allPlayers.Count > 0)
            {
                newId = allPlayers.Max(p => p.Id) + 1;
            }

            Player newPlayer = new Player();

            newPlayer.Id = newId;
            newPlayer.ClubId = clubId;
            newPlayer.Age = random.Next(18, 35);
            newPlayer.Defense = random.Next(0, 100);
            newPlayer.Midfield = random.Next(0, 100);
            newPlayer.Attack = random.Next(0, 100);
            newPlayer.Goalkeeping = random.Next(0, 100);
            newPlayer.Potential = random.Next(0, 300);
            newPlayer.FirstName = _personNameService.GetRandomFirstName(countryId);
            newPlayer.LastName = _personNameService.GetRandomLastName(countryId);
            newPlayer.CountryId = countryId;

            if (newPlayer.Age < 22)
            {
                newPlayer.ContractYears = 5;
            }
            else if (newPlayer.Age < 26)
            {
                newPlayer.ContractYears = 4;
            }
            else if (newPlayer.Age < 31)
            {
                newPlayer.ContractYears = 3;
            }
            else if (newPlayer.Age < 35)
            {
                newPlayer.ContractYears = 2;
            }
            else
            {
                newPlayer.ContractYears = 1;
            }

            return CreatePlayer(newPlayer);
        }

        public Player GenerateYouthPlayer(int clubId, int countryId)
        {
            var allPlayers = _playerRepository.Get();
            var newId = 0;
            var countryIdLastName = countryId;
            var youthRating = _clubService.GetClub(clubId).YouthFacilities;

            //randomize potential foreigner
            if (random.Next(0, 10) == 0)
            {
                var countries = _countryService.GetAllCountries();

                countryIdLastName = countries[random.Next(0, countries.Count)].Id;
            }
            else if (random.Next(0, 20) == 0)
            {
                var countries = _countryService.GetAllCountries();

                countryId = countries[random.Next(0, countries.Count)].Id;
                countryIdLastName = countryId;
            }

            if (allPlayers.Count > 0)
            {
                newId = allPlayers.Max(p => p.Id) + 1;
            }

            Player newPlayer = new Player();

            newPlayer.Id = newId;
            newPlayer.ClubId = clubId;
            newPlayer.Age = 18;
            newPlayer.Defense = random.Next(0, 50 + youthRating);
            newPlayer.Midfield = random.Next(0, 50 + youthRating);
            newPlayer.Attack = random.Next(0, 50 + youthRating);
            newPlayer.Goalkeeping = random.Next(0, 50 + youthRating);
            newPlayer.Potential = random.Next(0, 300 + +youthRating);
            newPlayer.FirstName = _personNameService.GetRandomFirstName(countryId);
            newPlayer.LastName = _personNameService.GetRandomLastName(countryIdLastName);
            newPlayer.CountryId = countryId;
            newPlayer.ContractYears = 2;
            newPlayer.Wage = 10;

            return CreatePlayer(newPlayer);
        }

        public Player CreatePlayer(Player player)
        {
            var allPlayers = _playerRepository.Get().Where(p => p.Id != player.Id).ToList();

            allPlayers.Add(player);
            _playerRepository.Create(allPlayers);

            return player;

        }

        public IList<Player> GetPlayers()
        {
            return _playerRepository.Get();
        }

        public IList<Player> GetPlayersFromClub(int clubId)
        {
            return _playerRepository.Get().Where(p => p.ClubId == clubId).ToList();
        }

        public IList<Player> UpdatePlayersEndOfSeason()
        {
            var allPlayers = _playerRepository.Get();
            var allManagers = _managerService.GetAllManagers();
            var playersToRetire = new List<Player>();
            var clubs = _clubService.GetAllClubs();

            foreach (var player in allPlayers)
            {
                player.Age++;
                player.ContractYears--;
                var manager = allManagers.FirstOrDefault(m => m.ClubId == player.ClubId);
                var trainingRating = clubs.FirstOrDefault(c => c.Id == player.ClubId).TrainingFacilities;

                if (player.Age > 30 && random.Next(0, 5) == 0)
                {
                    playersToRetire.Add(player);
                    continue;
                }

                if (player.Potential <= player.Defense + player.Midfield + player.Attack)
                {
                    continue;
                }

                player.Defense += random.Next(0, (manager.TrainingDefenseSkill / 10) + trainingRating);
                player.Midfield += random.Next(0, (manager.TrainingMidfieldSkill / 10) + trainingRating);
                player.Attack += random.Next(0, (manager.TrainingAttackSkill / 10) + trainingRating);
                player.Goalkeeping += random.Next(0, (manager.TrainingGoalkeepingSkill / 10) + trainingRating);
            }

            foreach (var player in playersToRetire)
            {
                allPlayers.Remove(player);
                //allPlayers.Add(GenerateYouthPlayer(player.ClubId, player.CountryId));
            }
            _playerRepository.Create(allPlayers);

            return allPlayers;
        }

        public void CheckIfClubHasEnoughPlayers(int clubId)
        {
            var players = _playerRepository.Get().Where(p => p.ClubId == clubId).ToList();
            var counter = players.Count;
            if (counter < 11)
            {
                var countryId = _clubService.GetClub(clubId).CountryId;
                for (int i = counter; i < 11; i++)
                {
                    GenerateYouthPlayer(clubId, countryId);
                }
            }
        }

        public Player GetPlayer(int playerId)
        {
            return _playerRepository.Get().FirstOrDefault(p => p.Id == playerId);
        }
    }
}
