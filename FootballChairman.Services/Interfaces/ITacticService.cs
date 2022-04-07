using FootballChairman.Models;

namespace FootballChairman.Services.Interfaces
{
    public interface ITacticService
    {
        public Tactic GetStandardTactic(int clubId);
        public Tactic CreateTactic(Tactic tactic);
        public Tactic GetTactic(int clubId);
    }
}
