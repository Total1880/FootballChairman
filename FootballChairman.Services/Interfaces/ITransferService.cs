using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface ITransferService
    {
        IList<Transfer> AddTransfers(IList<Transfer> transfers);
        IList<Transfer> GetTransferListOfClub(int clubId);
    }
}
