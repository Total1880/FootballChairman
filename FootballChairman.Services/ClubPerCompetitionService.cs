using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using OlavFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class ClubPerCompetitionService : IClubPerCompetitionService
    {
        IRepository<ClubPerCompetition> _clubPerCompetitionRepository;
        ICompetitionService _competitionService;

        public ClubPerCompetitionService(IRepository<ClubPerCompetition> clubPerCompetitionRepository, ICompetitionService competitionService)
        {
            _clubPerCompetitionRepository = clubPerCompetitionRepository;
            _competitionService = competitionService;
        }

        public ClubPerCompetition CreateClubPerCompetition(ClubPerCompetition clubPerCompetition)
        {
            var list = GetAll();
            list.Add(clubPerCompetition);
            _clubPerCompetitionRepository.Create(list);
            return clubPerCompetition;
        }

        public IList<ClubPerCompetition> GetAll()
        {
            return _clubPerCompetitionRepository.Get();
        }

        public void ResetData()
        {
            var list = GetAll();

            foreach (var item in list)
            {
                item.GoalsFor = 0;
                item.GoalsAgainst = 0;
                item.Points = 0;
                item.IsNew = false;
            }

            _clubPerCompetitionRepository.Create(list);
        }

        public void UpdateData(Game game)
        {
            var list = GetAll();
            var home = list.FirstOrDefault(c => c.ClubId == game.Fixture.HomeOpponentId && c.CompetitionId == game.Fixture.CompetitionId);
            var away = list.FirstOrDefault(c => c.ClubId == game.Fixture.AwayOpponentId && c.CompetitionId == game.Fixture.CompetitionId);

            home.GoalsFor += game.HomeScore;
            home.GoalsAgainst += game.AwayScore;
            away.GoalsAgainst += game.HomeScore;
            away.GoalsFor += game.AwayScore;

            if (game.HomeScore > game.AwayScore)
            {
                home.Points += Configuration.PointPerWin;
                away.Points += Configuration.PointPerLoss;
            }
            else if (game.HomeScore < game.AwayScore)
            {
                away.Points += Configuration.PointPerWin;
                home.Points += Configuration.PointPerLoss;
            }
            else
            {
                home.Points += Configuration.PointPerEqual;
                away.Points += Configuration.PointPerEqual;
            }

            _clubPerCompetitionRepository.Create(list);
        }

        public void UpdatePromotionsAndRelegations(IList<ClubPerCompetition> ranking)
        {
            var competition = _competitionService.GetAllCompetitions().FirstOrDefault(c => c.Id == ranking[0].CompetitionId);
            var list = GetAll();

            for (int i = 0; i < Configuration.PromotionSpots; i++)
            {
                if (competition.PromotionCompetitionId >= 0)
                {
                    list.FirstOrDefault(c => c.CompetitionId == competition.Id && c.ClubId == ranking[i].ClubId).IsNew = true;
                    list.FirstOrDefault(c => c.CompetitionId == competition.Id && c.ClubId == ranking[i].ClubId).CompetitionId = competition.PromotionCompetitionId;
                }
            }

            for (int i = ranking.Count; i > ranking.Count - Configuration.RelegationSpots; i--)
            {
                if (competition.RelegationCompetitionId >= 0)
                {
                    list.FirstOrDefault(c => c.CompetitionId == competition.Id && c.ClubId == ranking[i - 1].ClubId).IsNew = true;
                    list.FirstOrDefault(c => c.CompetitionId == competition.Id && c.ClubId == ranking[i - 1].ClubId).CompetitionId = competition.RelegationCompetitionId;
                }
            }

            _clubPerCompetitionRepository.Create(list);
        }
    }
}
