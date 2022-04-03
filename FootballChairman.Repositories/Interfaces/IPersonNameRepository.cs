using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Repositories.Interfaces
{
    public interface IPersonNameRepository
    {
        public IList<string> GetFirstNames(int countryId);
        public IList<string> GetLastNames(int countryId);
    }
}
