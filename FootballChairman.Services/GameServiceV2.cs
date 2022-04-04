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
        private readonly IRepository<Game> _gameRepository;
        private readonly IRepository<Fixture> _fixtureRepository;

        public GameServiceV2(
            IRepository<Game> gameRepository, 
            IRepository<Fixture> fixtureRepository)
        {
            _gameRepository = gameRepository;
            _fixtureRepository = fixtureRepository;
        }

        public Game PlayGame(Fixture fixture, bool suddendeath, Tactic homeTactic, Tactic awayTactic)
        {
            var game = new Game();
            game.Fixture = fixture;

            double homeSkills = 0;
            double awaySkills = 0;

            double homeAttack = 0;
            double homeDefense = homeTactic.Goalkeeper.Goalkeeping;
            double awayAttack = 0;
            double awayDefense = awayTactic.Goalkeeper.Goalkeeping;

            foreach (var player in homeTactic.Defenders)
            {
                homeSkills += player.Defense;
                homeDefense += player.Defense;
            }
            foreach (var player in homeTactic.Midfielders)
            {
                homeSkills += player.Midfield;
                homeDefense += player.Midfield / 2;
                homeAttack += player.Midfield / 2;
            }
            foreach (var player in homeTactic.Attackers)
            {
                homeSkills += player.Attack;
                homeAttack += player.Attack;
            }

            foreach (var player in awayTactic.Defenders)
            {
                awaySkills += player.Defense;
                awayDefense += player.Defense;
            }
            foreach (var player in awayTactic.Midfielders)
            {
                awaySkills += player.Midfield;
                awayDefense += player.Midfield / 2;
                awayAttack += player.Midfield / 2;
            }
            foreach (var player in awayTactic.Attackers)
            {
                awaySkills += player.Attack;
                awayAttack += player.Attack;
            }

            homeSkills /= 11;
            awaySkills /= 11;
            homeAttack /= homeTactic.Attackers.Count + homeTactic.Midfielders.Count / 2;
            homeDefense /= homeTactic.Defenders.Count + homeTactic.Midfielders.Count / 2 + 1;
            awayAttack /= awayTactic.Attackers.Count + awayTactic.Midfielders.Count / 2;
            awayDefense /= awayTactic.Defenders.Count + awayTactic.Midfielders.Count / 2 + 1;

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
