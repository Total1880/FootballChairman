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

        public PlayerService(IRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Player GenerateRandomPlayer(int clubId)
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
            newPlayer.FirstName = "First" + newId;
            newPlayer.LastName = "Last" + newId;

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
    }
}
