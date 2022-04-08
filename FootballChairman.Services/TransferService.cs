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
    public class TransferService : ITransferService
    {
        private readonly IRepository<Transfer> _transferRepository;
        private readonly IRepository<Club> _clubRepository;
        private readonly IRepository<Player> _playerRepository;

        public TransferService(IRepository<Transfer> transferRepository, IRepository<Player> playerRepository, IRepository<Club> clubRepository)
        {
            _transferRepository = transferRepository;
            _playerRepository = playerRepository;
            _clubRepository = clubRepository;
        }

        public IList<Transfer> AddTransfers(IList<Transfer> transfers)
        {
            var allTransfers = _transferRepository.Get().ToList();
            allTransfers.AddRange(transfers);
            _transferRepository.Create(allTransfers);

            return allTransfers;
        }

        public IList<Transfer> GetTransferListOfClub(int clubId)
        {
            var transfers = _transferRepository.Get().Where(t => t.PreviousClub.Id == clubId || t.NextClub.Id == clubId).ToList();
            var players = _playerRepository.Get();
            var clubs = _clubRepository.Get();

            foreach (var transfer in transfers)
            {
                transfer.Player = players.FirstOrDefault(p => p.Id == transfer.Player.Id);
                transfer.PreviousClub = clubs.FirstOrDefault(c => c.Id == transfer.PreviousClub.Id);
                transfer.NextClub = clubs.FirstOrDefault(c => c.Id == transfer.NextClub.Id);
            }
            DeleteTransfers(transfers.Where(t => t.Player == null).ToList());
            return transfers.Where(t => t.Player != null).ToList();
        }

        private void DeleteTransfers(IList<Transfer> transfers)
        {
            var allTransfers = _transferRepository.Get().ToList();

            foreach (var transfer in transfers)
            {
                allTransfers.Remove(allTransfers.FirstOrDefault(t => t.Year == transfer.Year && t.Player == null));
            }
            _transferRepository.Create(allTransfers);

        }
    }
}
