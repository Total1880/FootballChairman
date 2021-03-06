using FootballChairman.Repositories.Interfaces;
using FootballChairman.Services.Interfaces;

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

        private static int RandomInt(int min, int max)
        {
            Random random = new Random();
            int val = random.Next(min, max);
            return val;
        }
    }
}
