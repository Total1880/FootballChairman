namespace FootballChairman.Repositories
{
    public interface IRepository<T>
    {
        IList<T> Create(IList<T> itemList);
        IList<T> Get();
    }
}
