﻿using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Services.Interfaces;
using OlavFramework;
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
        private readonly ISaveGameDataService _saveGameDataService;
        private readonly IClubService _clubService;
        private readonly IManagerService _managerService;
        private readonly IHistoryItemService _historyItemService;

        private IList<Competition> _competitions;
        private IList<CompetitionCup> _competitionCups;
        private SaveGameData _saveGameData;

        public GameEngineService(
            ICompetitionService competitionService,
            ICompetitionCupService competitionCupService,
            IFixtureService fixtureService, IGameService gameService,
            IClubPerCompetitionService clubPerCompetitionService,
            ISaveGameDataService saveGameDataService,
            IClubService clubService,
            IManagerService managerService, IHistoryItemService historyItemService)
        {
            _competitionService = competitionService;
            _competitionCupService = competitionCupService;
            _fixtureService = fixtureService;
            _gameService = gameService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _saveGameDataService = saveGameDataService;
            _clubService = clubService;
            _managerService = managerService;
            _historyItemService = historyItemService;

            _competitions = _competitionService.GetAllCompetitions();
            _competitionCups = _competitionCupService.GetAllCompetitions();
            _saveGameData = _saveGameDataService.GetSaveGameData(Configuration.DefaultSaveGameName);
        }

        public void ProcessMatchDay()
        {
            var fixtureList = _fixtureService.LoadFixturesOfMatchday(_saveGameData.MatchDay);
            var fixturesLeft = false;

            foreach (var competition in _competitions)
            {
                foreach (var fixture in fixtureList.Where(f => f.CompetitionId == competition.Id))
                {
                    var game = _gameService.PlayGame(fixture);
                    _clubPerCompetitionService.UpdateData(game);
                    fixturesLeft = true;
                }
            }

            foreach (var competitionCup in _competitionCups)
            {
                foreach (var fixture in fixtureList.Where(f => f.CompetitionId == competitionCup.Id))
                {
                    var game = _gameService.PlayGame(fixture);
                    _clubPerCompetitionService.UpdateCupData(game);
                    _fixtureService.UpdateCupData(game, _saveGameData);
                    fixturesLeft = true;

                }
            }
            _saveGameData.MatchDay++;
            _saveGameDataService.CreateSaveGameData(_saveGameData);

            if (!fixturesLeft)
            {
                _saveGameData.MatchDay = Configuration.WeeksInYear + 1;
                ProcessEndOfSeason();
            }
        }

        public void GoToEndOfSeason()
        {
            while (_saveGameData.MatchDay <= Configuration.WeeksInYear)
            {
                ProcessMatchDay();
            }
        }

        private void ProcessEndOfSeason()
        {
            if (_saveGameData.MatchDay <= Configuration.WeeksInYear)
                return;

            _clubService.UpdateClubsEndOfSeasonTroughManager();
            _clubService.UpdateClubsWithNewManagers(_managerService.UpdateManagersEndSeason());

            var competitions = _competitionService.GetAllCompetitions().Where(com => com.CompetitionType != CompetitionType.NationalCup);

            foreach (var competition in competitions)
            {
                var ranking = _clubPerCompetitionService.GetAll()
                    .Where(c => c.CompetitionId == competition.Id && !c.IsNew)
                    .OrderBy(c => c.ClubName)
                    .OrderByDescending(c => c.GoalDifference)
                    .OrderByDescending(c => c.Points)
                    .ToList();

                if (ranking == null || ranking.Count() < 1)
                    continue;

                _historyItemService.CreateHistoryItem(new HistoryItem { ClubId = ranking[0].ClubId, CompetitionId = competition.Id, Year = _saveGameData.Year });
                _clubPerCompetitionService.UpdatePromotionsAndRelegations(ranking);
            }
            _gameService.CleanGames();
            CreateInternationalFixtures();
            ResetFixtures();
            _clubPerCompetitionService.ResetData();

            _saveGameData.MatchDay = 0;
            _saveGameData.Year++;
            _saveGameDataService.CreateSaveGameData(_saveGameData);
        }

        private void CreateInternationalFixtures()
        {
            //Create Champions league
            var clubs = new List<Club>();
            foreach (var competition in _competitionService.GetAllCompetitions().Where(comp => comp.CompetitionType == CompetitionType.NationalCompetition && comp.PromotionCompetitionId == -1))
            {
                var historyItem = _historyItemService.GetHistoryItemsOfCompetition(competition.Id).FirstOrDefault(hi => hi.Year == _saveGameData.Year);
                clubs.Add(_clubService.GetClub(historyItem.ClubId));
            }

            _clubPerCompetitionService.CreateInternationalClubPerCompetitions(clubs, _competitionService.GetAllCompetitions().FirstOrDefault(c => c.CompetitionType == CompetitionType.InternationalCompetition).Id);

            //Create Cup Winners Cup
            clubs = new List<Club>();
            foreach (var competition in _competitionCupService.GetAllCompetitions().Where(comp => comp.CompetitionType == CompetitionType.NationalCup))
            {
                var historyItem = _historyItemService.GetHistoryItemsOfCompetition(competition.Id).FirstOrDefault(hi => hi.Year == _saveGameData.Year);
                clubs.Add(_clubService.GetClub(historyItem.ClubId));
            }

            _clubPerCompetitionService.CreateInternationalClubPerCompetitions(clubs, _competitionCupService.GetAllCompetitions().FirstOrDefault(c => c.CompetitionType == CompetitionType.InternationalCup).Id);
        }

        private void ResetFixtures()
        {
            var clubs = _clubService.GetAllClubs();
            var competitions = _competitionService.GetAllCompetitions().Where(com => com.CompetitionType != CompetitionType.NationalCup);
            var clubsPerCompetition = _clubPerCompetitionService.GetAll();

            foreach (var competition in competitions)
            {
                var listOfClubs = new List<Club>();

                foreach (var club in clubsPerCompetition.Where(c => c.CompetitionId == competition.Id))
                {
                    listOfClubs.Add(clubs.FirstOrDefault(c => c.Id == club.ClubId));
                }

                _fixtureService.GenerateFixtures(listOfClubs, competition.Id);
            }

            var competitionsCup = _competitionCupService.GetAllCompetitions();
            foreach (var competition in competitionsCup)
            {
                var listOfClubs = new List<Club>();

                foreach (var club in clubsPerCompetition.Where(c => c.CompetitionId == competition.Id).OrderByDescending(c => c.ClubId))
                {
                    listOfClubs.Add(clubs.FirstOrDefault(c => c.Id == club.ClubId));
                }
                _fixtureService.GenerateCupFixtures(listOfClubs, competition);
            }
        }
    }
}
