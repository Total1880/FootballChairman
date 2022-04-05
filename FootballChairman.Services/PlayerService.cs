using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class PlayerService : IPlayerService
    {
        private Random random = new Random();
        private readonly IRepository<Player> _playerRepository;
        private readonly IPersonNameService _personNameService;
        private readonly ICountryService _countryService;
        private readonly IClubService _clubService;

        public PlayerService(IRepository<Player> playerRepository, IPersonNameService personNameService, ICountryService countryService, IClubService clubService)
        {
            _playerRepository = playerRepository;
            _personNameService = personNameService;
            _countryService = countryService;
            _clubService = clubService;
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

            return CreatePlayer(newPlayer);
        }

        public Player GenerateYouthPlayer(int clubId, int countryId)
        {
            var allPlayers = _playerRepository.Get();
            var newId = 0;
            var countryIdLastName = countryId;

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
            newPlayer.Defense = random.Next(0, 50);
            newPlayer.Midfield = random.Next(0, 50);
            newPlayer.Attack = random.Next(0, 50);
            newPlayer.Goalkeeping = random.Next(0, 50);
            newPlayer.Potential = random.Next(0, 300);
            newPlayer.FirstName = _personNameService.GetRandomFirstName(countryId);
            newPlayer.LastName = _personNameService.GetRandomLastName(countryIdLastName);
            newPlayer.CountryId = countryId;

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
            var playersToRetire = new List<Player>();

            foreach (var player in allPlayers)
            {
                player.Age++;

                if (player.Age > 30 && random.Next(0, 5) == 0)
                {
                    playersToRetire.Add(player);
                    continue;
                }

                if (player.Potential <= player.Defense + player.Midfield + player.Attack)
                    continue;

                player.Defense += random.Next(0, 5);
                player.Midfield += random.Next(0, 5);
                player.Attack += random.Next(0, 5);
                player.Goalkeeping += random.Next(0, 5);
            }

            foreach (var player in playersToRetire)
            {
                allPlayers.Remove(player);
                allPlayers.Add(GenerateYouthPlayer(player.ClubId, player.CountryId));
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
    }
}
