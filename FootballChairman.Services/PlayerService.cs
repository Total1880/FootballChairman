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

        public PlayerService(IRepository<Player> playerRepository, IPersonNameService personNameService)
        {
            _playerRepository = playerRepository;
            _personNameService = personNameService;
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
            newPlayer.Age = random.Next(18,35);
            newPlayer.Defense = random.Next(0, 100);
            newPlayer.Midfield = random.Next(0, 100);
            newPlayer.Attack = random.Next(0, 100);
            newPlayer.Goalkeeping = random.Next(0, 100);
            newPlayer.FirstName = _personNameService.GetRandomFirstName(countryId);
            newPlayer.LastName = _personNameService.GetRandomLastName(countryId);
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
                player.Defense += random.Next(0, 5);
                player.Midfield += random.Next(0, 5);
                player.Attack += random.Next(0, 5);
                player.Goalkeeping += random.Next(0, 5);
                player.Age++;

                if (player.Age > 30 && random.Next(0,5) == 0)
                    playersToRetire.Add(player);
            }

            foreach (var player in playersToRetire)
            {
                allPlayers.Remove(player);
                allPlayers.Add(GenerateRandomPlayer(player.ClubId, player.CountryId));
            }
            _playerRepository.Create(allPlayers);

            return allPlayers;
        }
    }
}
