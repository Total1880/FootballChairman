using FootballChairman.Datafiller;
using FootballChairman.Models;
using FootballChairman.Models.Enums;
using FootballChairman.Services.Interfaces;
using GalaSoft.MvvmLight;
using OlavFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FootballChairman.ViewModels
{
    public class FixturesPageViewModel : ViewModelBase
    {
        private readonly IFixtureService _fixtureService;
        private readonly ICompetitionService _competitionService;
        private readonly IClubService _clubService;
        private readonly IClubPerCompetitionService _clubPerCompetitionService;
        private readonly IManagerService _managerService;
        private readonly ICountryService _countryService;
        private readonly ICompetitionCupService _competitionCupService;
        private readonly IPlayerService _playerService;
        private readonly ISaveGameDataService _saveGameDataService;

        public FixturesPageViewModel(IFixtureService fixtureService,
            ICompetitionService competitionService,
            IClubService clubService,
            IClubPerCompetitionService clubPerCompetitionService,
            IManagerService managerService,
            ICountryService countryService,
            ICompetitionCupService competitionCupService,
            IPlayerService playerService, 
            ISaveGameDataService saveGameDataService)
        {
            _fixtureService = fixtureService;
            _competitionService = competitionService;
            _clubService = clubService;
            _clubPerCompetitionService = clubPerCompetitionService;
            _managerService = managerService;
            _countryService = countryService;
            _competitionCupService = competitionCupService;
            _playerService = playerService;
            _saveGameDataService = saveGameDataService;

            if (!_saveGameDataService.DoesSaveGameExist(Configuration.DefaultSaveGameName))
            {
                var data = new AddData(_fixtureService, _competitionService, _clubService, _clubPerCompetitionService, _managerService, _countryService, _competitionCupService, _playerService);
            }
        }
    }
}
