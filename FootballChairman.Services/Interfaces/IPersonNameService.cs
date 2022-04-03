using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IPersonNameService
    {
        public string GetRandomFirstName(int countryId);
        public string GetRandomLastName(int countryId);   
    }
}
