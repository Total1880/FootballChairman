using FootballChairman.Repositories.Interfaces;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class PersonNameService : IPersonNameService
    {
        private readonly IPersonNameRepository _personNameRepository;

        public PersonNameService(IPersonNameRepository personNameRepository)
        {
            _personNameRepository = personNameRepository;
        }
        public string GetRandomFirstName(int countryId)
        {
            var firstNames = _personNameRepository.GetFirstNames(countryId);
            return firstNames[RandomInt(0, firstNames.Count)];
        }

        public string GetRandomLastName(int countryId)
        {
            var lastNames = _personNameRepository.GetLastNames(countryId);
            return lastNames[RandomInt(0, lastNames.Count)];
        }

        static int RandomInt(int min, int max)
        {
            Random random = new Random();
            int val = random.Next(min, max);
            return val;
        }
    }
}
