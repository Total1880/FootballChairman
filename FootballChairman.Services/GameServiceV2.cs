using FootballChairman.Models;
using FootballChairman.Repositories;
using FootballChairman.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services
{
    public class GameServiceV2 : IGameService
    {
        private readonly IClubService _clubService;
        private readonly IPlayerService _playerService;
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Fixture> _fixtureRepository;

        public GameServiceV2(IClubService clubService, IPlayerService playerService, IRepository<Game> gameRepository, IRepository<Fixture> fixtureRepository)
        {
            _clubService = clubService;
            _playerService = playerService;
            _gameRepository = gameRepository;
            _fixtureRepository = fixtureRepository;
        }

        public Game PlayGame(Fixture fixture, bool suddendeath)
        {
            var game = new Game();
            game.Fixture = fixture;

            var homeClub = _clubService.GetClub(fixture.HomeOpponentId);
            var awayClub = _clubService.GetClub(fixture.AwayOpponentId);

            var homePlayers = _playerService.GetPlayersFromClub(homeClub.Id);
            var awayPlayers = _playerService.GetPlayersFromClub(awayClub.Id);

            //var homeSkills = ((homeClub.SkillAttack + homeClub.SkillMidfield + homeClub.SkillDefense) / 3) * (11 - homePlayers.Count);
            //var awaySkills = ((awayClub.SkillAttack + awayClub.SkillMidfield + awayClub.SkillDefense) / 3) * (11- awayPlayers.Count);
            double homeSkills = 0;
            double awaySkills = 0;

            //var homeAttack = ((homeClub.SkillAttack + homeClub.SkillMidfield / 2) / 1.5) * (11 - homePlayers.Count);
            //var homeDefense = ((homeClub.SkillDefense + homeClub.SkillMidfield / 2) / 1.5) * (11 - homePlayers.Count);
            //var awayAttack = ((awayClub.SkillAttack + awayClub.SkillMidfield / 2) / 1.5) * (11 - awayPlayers.Count);
            //var awayDefense = ((awayClub.SkillDefense + awayClub.SkillMidfield / 2) / 1.5) * (11 - awayPlayers.Count);
            double homeAttack = 0;
            double homeDefense = 0;
            double awayAttack = 0;
            double awayDefense = 0;

            foreach (var player in homePlayers)
            {
                homeSkills = homeSkills + ((player.Defense + player.Midfield + player.Attack) / 3);
                homeAttack = homeAttack + ((player.Attack + player.Midfield / 2) / 1.5);
                homeDefense = homeDefense + ((player.Defense + player.Midfield / 2) / 1.5);
            }
            foreach (var player in awayPlayers)
            {
                awaySkills = awaySkills + ((player.Defense + player.Midfield + player.Attack) / 3);
                awayAttack = awayAttack + ((player.Attack + player.Midfield / 2) / 1.5);
                awayDefense = awayDefense + ((player.Defense + player.Midfield / 2) / 1.5);
            }

            homeSkills /= 11;
            awaySkills /= 11;
            homeAttack /= 11;
            homeDefense /= 11;
            awayAttack /= 11;
            awayDefense /= 11;

            var skillDifference = homeSkills - awaySkills;
            var homeAttackDifference = homeAttack - awayDefense;
            var awayAttackDifference = awayAttack - homeDefense;

            var homeChange = 50 + skillDifference / 2;
            var awayChange = 50 - skillDifference / 2;

            var homeScoreChange = 33 + homeAttackDifference / 3;
            var awayScoreChange = 33 + awayAttackDifference / 3;

            if (homeScoreChange < 0)
            {
                throw new Exception();
            }
            if (awayScoreChange < 0)
            {
                throw new Exception();
            }

            for (int i = 0; i < 90; i++)
            {
                if (RandomInt(0, 6) == 0)
                {
                    if (RandomInt(0, 100) < homeChange)
                    {
                        if (RandomInt(0, 100) < homeScoreChange)
                        {
                            game.HomeScore++;
                        }
                    }
                    else
                    {
                        if (RandomInt(0, 100) < awayScoreChange)
                        {
                            game.AwayScore++;
                        }
                    }
                }
            }

            if (game.HomeScore == game.AwayScore && suddendeath)
            {
                if (RandomInt(1, 3) == 1)
                    game.HomeScore++;
                else
                    game.AwayScore++;
            }

            return SaveGame(game);
        }

        //public Game PlayGame(Fixture fixture, bool suddendeath)
        //{
        //    var game = new Game();
        //    game.Fixture = fixture;

        //    var homeClub = _clubService.GetClub(fixture.HomeOpponentId);
        //    var awayClub = _clubService.GetClub(fixture.AwayOpponentId);
        //    var homeMidfieldStronger = homeClub.SkillMidfield >= awayClub.SkillMidfield;
        //    var differenceMidfield = homeMidfieldStronger ? homeClub.SkillMidfield - awayClub.SkillMidfield : awayClub.SkillMidfield - homeClub.SkillMidfield;

        //    for (int i = 0; i < 90; i++)
        //    {
        //        if (RandomInt(0, (int)Math.Round((decimal)differenceMidfield/2)) == 0)
        //        {
        //            // chance stronger team
        //            if (homeMidfieldStronger)
        //            {
        //                //chance hometeam
        //                if (RandomInt(0, homeClub.SkillAttack) != 0)
        //                {
        //                    // Succesfull attack, other team may now defend
        //                    if (RandomInt(0, (int)Math.Round((decimal)awayClub.SkillDefense / 3)) == 0)
        //                    {
        //                        // Defence fails, goal
        //                        game.HomeScore++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //chance awayteam
        //                if (RandomInt(0, awayClub.SkillAttack) != 0)
        //                {
        //                    // Succesfull attack, other team may now defend
        //                    if (RandomInt(0, (int)Math.Round((decimal)homeClub.SkillDefense / 3)) == 0)
        //                    {
        //                        // Defence fails, goal
        //                        game.AwayScore++;
        //                    }
        //                }
        //            }
        //        }
        //        else if (RandomInt(0, differenceMidfield) == 0)
        //        {
        //            //change weaker team
        //            if (homeMidfieldStronger)
        //            {
        //                //chance awayteam
        //                if (RandomInt(0, awayClub.SkillAttack) != 0)
        //                {
        //                    // Succesfull attack, other team may now defend
        //                    if (RandomInt(0, (int)Math.Round((decimal)homeClub.SkillDefense / 3)) == 0)
        //                    {
        //                        // Defence fails, goal
        //                        game.AwayScore++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //chance hometeam
        //                if (RandomInt(0, homeClub.SkillAttack) != 0)
        //                {
        //                    // Succesfull attack, other team may now defend
        //                    if (RandomInt(0, (int)Math.Round((decimal)awayClub.SkillDefense / 3)) == 0)
        //                    {
        //                        // Defence fails, goal
        //                        game.HomeScore++;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (game.HomeScore == game.AwayScore && suddendeath)
        //    {
        //        if (RandomInt(1, 3) == 1)
        //            game.HomeScore++;
        //        else
        //            game.AwayScore++;
        //    }
        //    return SaveGame(game);
        //}

        static int RandomInt(int min, int max)
        {
            Random random = new Random();
            int val = random.Next(min, max);
            return val;
        }

        public IList<Game> GetGames()
        {
            var games = _gameRepository.Get();
            var fixtures = _fixtureRepository.Get();

            foreach (var game in games)
            {
                game.Fixture = fixtures.FirstOrDefault(f => f.IdString == game.FixtureId);
                if (game.Fixture == null)
                {
                    game.Fixture = new Fixture();
                }
            }

            return games;
        }

        private Game SaveGame(Game game)
        {
            var games = GetGames();

            games.Add(game);
            _gameRepository.Create(games);

            return game;
        }

        public void CleanGames()
        {
            var list = new List<Game>();
            list.Add(new Game { Fixture = new Fixture { CompetitionId = -1 } });
            _gameRepository.Create(list);
        }
    }
}
