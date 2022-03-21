using FootballChairman.Models;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class GameEngineService : IGameEngineService
    {
        private readonly ICompetitionService _competitionService;
        private readonly ICompetitionCupService _competitionCupService;
        private readonly IFixtureService _fixtureService;
        private readonly IGameService _gameService;
        private readonly IClubPerCompetitionService _clubPerCompetitionService;

        private IList<Competition> _competitions;
        private IList<CompetitionCup> _competitionCups;
        private SaveGameData _saveGameData;

        public GameEngineService(ICompetitionService competitionService, ICompetitionCupService competitionCupService, IFixtureService fixtureService, IGameService gameService, IClubPerCompetitionService clubPerCompetitionService)
        {
            _competitionService = competitionService;
            _competitionCupService = competitionCupService;
            _fixtureService = fixtureService;
            _gameService = gameService;
            _clubPerCompetitionService = clubPerCompetitionService;

            _competitions = _competitionService.GetAllCompetitions();
            _competitionCups = _competitionCupService.GetAllCompetitions();
            _saveGameData = new SaveGameData();
        }
        public void ProcessMatchDay()
        {
            var fixtureList = _fixtureService.LoadFixturesOfMatchday(_saveGameData.MatchDay);

            foreach (var competition in _competitions)
            {
                foreach (var fixture in fixtureList.Where(f => f.CompetitionId == competition.Id))
                {
                    var game = _gameService.PlayGame(fixture);
                    _clubPerCompetitionService.UpdateData(game);
                }
            }

            foreach (var competitionCup in _competitionCups)
            {
                foreach (var fixture in fixtureList.Where(f => f.CompetitionId == competitionCup.Id))
                {
                    var game = _gameService.PlayGame(fixture);
                    _clubPerCompetitionService.UpdateCupData(game);
                    _fixtureService.UpdateCupData(game, _saveGameData);
                }
            }
            _saveGameData.MatchDay++;
        }
    }
}
