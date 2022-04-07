namespace FootballChairman.Repositories.Interfaces
{
    public interface IPersonNameRepository
    {
        public IList<string> GetFirstNames(int countryId);
        public IList<string> GetLastNames(int countryId);
    }
}
