using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class GameService : IGameService
    {
        private readonly IClubService _clubService;
        private readonly ICompetitionService _competitionService;

        public GameService(IClubService clubService, ICompetitionService competitionService)
        {
            _clubService = clubService;
            _competitionService = competitionService;
        }
        public Game PlayGame(Fixture fixture)
        {
            var game = new Game();
            int homeScore;
            int awayScore;
            int homeScoreCompensation = 0;
            int awayScoreCompensation = 0;
            int highestSkill = Math.Max(_clubService.GetClub(fixture.HomeOpponentId).Skill, _clubService.GetClub(fixture.AwayOpponentId).Skill);
            var competition = _competitionService.GetAllCompetitions().FirstOrDefault(com => com.Id == fixture.CompetitionId);
            game.Fixture = fixture;

            homeScore = RandomInt(0, 6) + _clubService.GetClub(fixture.HomeOpponentId).Skill - highestSkill;
            awayScore = RandomInt(0, 6) + _clubService.GetClub(fixture.AwayOpponentId).Skill - highestSkill - 1;

            if (homeScore < 0)
                awayScoreCompensation = 0 - homeScore;

            if (awayScore < 0)
                homeScoreCompensation = 0 - awayScore;

            game.HomeScore = homeScore + homeScoreCompensation;
            game.AwayScore = awayScore + awayScoreCompensation;

            return game;
        }

        static int RandomInt(int min, int max)
        {
            Random random = new Random();
            int val = random.Next(min, max);
            return val;
        }
    }
}
