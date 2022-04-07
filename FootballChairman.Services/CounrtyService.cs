using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class CounrtyService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;

        public CounrtyService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public Country CreateCountry(Country country)
        {
            var list = _countryRepository.Get();

            if (list.Any(c => c.Id == country.Id))
            {
                list.Remove(list.FirstOrDefault(c => c.Id == country.Id));
            }

            list.Add(country);

            _countryRepository.Create(list);
            return country;
        }

        public IList<Country> GetAllCountries()
        {
            return _countryRepository.Get();
        }

        public Country GetCountry(int id)
        {
            return _countryRepository.Get().FirstOrDefault(x => x.Id == id);
        }
    }
}
