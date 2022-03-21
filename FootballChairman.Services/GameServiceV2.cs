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
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Fixture> _fixtureRepository;

        public GameServiceV2(IClubService clubService, IRepository<Game> gameRepository, IRepository<Fixture> fixtureRepository)
        {
            _clubService = clubService;
            _gameRepository = gameRepository;
            _fixtureRepository = fixtureRepository;
        }

        public Game PlayGame(Fixture fixture)
        {
            var game = new Game();
            game.Fixture = fixture;

            var homeClub = _clubService.GetClub(fixture.HomeOpponentId);
            var awayClub = _clubService.GetClub(fixture.AwayOpponentId);
            var homeMidfieldStronger = homeClub.SkillMidfield >= awayClub.SkillMidfield;
            var differenceMidfield = homeMidfieldStronger ? homeClub.SkillMidfield - awayClub.SkillMidfield : awayClub.SkillMidfield - homeClub.SkillMidfield;

            for (int i = 0; i < 90; i++)
            {
                if (RandomInt(0, (int)Math.Round((decimal)differenceMidfield/2)) == 0)
                {
                    // chance stronger team
                    if (homeMidfieldStronger)
                    {
                        //chance hometeam
                        if (RandomInt(0, homeClub.SkillAttack) != 0)
                        {
                            // Succesfull attack, other team may now defend
                            if (RandomInt(0, (int)Math.Round((decimal)awayClub.SkillDefense / 3)) == 0)
                            {
                                // Defence fails, goal
                                game.HomeScore++;
                            }
                        }
                    }
                    else
                    {
                        //chance awayteam
                        if (RandomInt(0, awayClub.SkillAttack) != 0)
                        {
                            // Succesfull attack, other team may now defend
                            if (RandomInt(0, (int)Math.Round((decimal)homeClub.SkillDefense / 3)) == 0)
                            {
                                // Defence fails, goal
                                game.AwayScore++;
                            }
                        }
                    }
                }
                else if (RandomInt(0, differenceMidfield) == 0)
                {
                    //change weaker team
                    if (homeMidfieldStronger)
                    {
                        //chance awayteam
                        if (RandomInt(0, awayClub.SkillAttack) != 0)
                        {
                            // Succesfull attack, other team may now defend
                            if (RandomInt(0, (int)Math.Round((decimal)homeClub.SkillDefense / 3)) == 0)
                            {
                                // Defence fails, goal
                                game.AwayScore++;
                            }
                        }
                    }
                    else
                    {
                        //chance hometeam
                        if (RandomInt(0, homeClub.SkillAttack) != 0)
                        {
                            // Succesfull attack, other team may now defend
                            if (RandomInt(0, (int)Math.Round((decimal)awayClub.SkillDefense / 3)) == 0)
                            {
                                // Defence fails, goal
                                game.HomeScore++;
                            }
                        }
                    }
                }
            }

            return SaveGame(game);
        }

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
    }
}
