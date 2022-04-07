using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class HistoryItemService : IHistoryItemService
    {
        private readonly IRepository<HistoryItem> _historyItemRepository;

        public HistoryItemService(IRepository<HistoryItem> historyItemRepository)
        {
            _historyItemRepository = historyItemRepository;
        }
        public HistoryItem CreateHistoryItem(HistoryItem historyItem)
        {
            var list = _historyItemRepository.Get();

            if (list.Any(hi => hi.Year == historyItem.Year && hi.CompetitionId == historyItem.CompetitionId))
            {
                throw new Exception("item already exist!");
            }

            if (list.Any())
            {
                historyItem.Id = list.Max(hi => hi.Id) + 1;
            }
            else
            {
                historyItem.Id = 0;
            }

            list.Add(historyItem);
            _historyItemRepository.Create(list);

            return historyItem;
        }

        public IList<HistoryItem> GetHistoryItemsOfCompetition(int competitionId)
        {
            return _historyItemRepository.Get().Where(hi => hi.CompetitionId == competitionId).ToList();
        }
    }
}
