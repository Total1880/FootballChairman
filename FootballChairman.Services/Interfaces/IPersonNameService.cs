namespace FootballChairman.Services.Interfaces
{
    public interface IPersonNameService
    {
        public string GetRandomFirstName(int countryId);
        public string GetRandomLastName(int countryId);
    }
}
