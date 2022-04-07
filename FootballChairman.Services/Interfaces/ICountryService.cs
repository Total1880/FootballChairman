using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface ICountryService
    {
        Country CreateCountry(Country country);
        IList<Country> GetAllCountries();
        Country GetCountry(int id);
    }
}
