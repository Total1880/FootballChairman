using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface ICountryService
    {
        Country CreateCountry(Country country);
        IList<Country> GetAllCountries();
        Country GetCountry(int id);
    }
}
