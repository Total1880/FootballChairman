using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;

namespace FootballChairman.Services
{
    public class ClubServiceV2 : IClubService
    {
        private readonly IRepository<Club> _clubRepository;
        private readonly IManagerService _managerService;

        public ClubServiceV2(IRepository<Club> clubRepository, IManagerService managerService)
        {
            _clubRepository = clubRepository;
            _managerService = managerService;
        }

        public IList<Club> CreateAllClubs(IList<Club> clubs)
        {
            return _clubRepository.Create(clubs);
        }

        public Club CreateClub(Club club)
        {
            var list = GetAllClubs();
            list.Add(club);
            _clubRepository.Create(list);
            return club;
        }

        public IList<Club> GetAllClubs()
        {
            return _clubRepository.Get();
        }

        public Club GetClub(int id)
        {
            return GetAllClubs().FirstOrDefault(c => c.Id == id);
        }

        public Club UpdateClub(Club club)
        {
            var list = GetAllClubs().Where(c => c.Id != club.Id).ToList();

            list.Add(club);

            return CreateAllClubs(list).FirstOrDefault(c => c.Id == club.Id);
        }

        public void UpdateClubsEndOfSeason(IList<ClubPerCompetition> ranking, int competitionReputation)
        {
            var clubs = GetAllClubs();
            var counter = ranking.Count();
            foreach (var club in ranking)
            {
                var lookUpClub = clubs.FirstOrDefault(c => c.Id == club.ClubId);
                if (lookUpClub == null)
                {
                    continue;
                }

                if (lookUpClub.Reputation < competitionReputation - 100)
                {
                    lookUpClub.Reputation += counter;
                }
                if (lookUpClub.Reputation < competitionReputation - 1000)
                {
                    lookUpClub.Reputation += 100;
                }
                if (lookUpClub.Reputation < competitionReputation - 2000)
                {
                    lookUpClub.Reputation += 500;
                }
                if (lookUpClub.Reputation > competitionReputation + 100)
                {
                    lookUpClub.Reputation -= counter;
                }
                if (lookUpClub.Reputation > competitionReputation + 1000)
                {
                    lookUpClub.Reputation -= 100;
                }
                if (lookUpClub.Reputation > competitionReputation + 2000)
                {
                    lookUpClub.Reputation -= 500;
                }


                UpdateClub(lookUpClub);
                counter--;
            }
        }

        public void UpdateClubsWithNewManagers(IList<Manager> newManagers)
        {
            if (newManagers.Count > 0)
            {
                foreach (var manager in newManagers)
                {
                    var club = GetClub(manager.ClubId);
                    club.ManagerId = manager.Id;
                    UpdateClub(club);
                }
            }
        }
    }
}
