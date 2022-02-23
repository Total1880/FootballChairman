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

        public ClubPerCompetitionService(IRepository<ClubPerCompetition> clubPerCompetitionRepository)
        {
            _clubPerCompetitionRepository = clubPerCompetitionRepository;
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
            else if(game.HomeScore < game.AwayScore)
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
    }
}
