using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface IHistoryItemService
    {
        HistoryItem CreateHistoryItem(HistoryItem historyItem);
        IList<HistoryItem> GetHistoryItemsOfCompetition(int competitionId);
    }
}
