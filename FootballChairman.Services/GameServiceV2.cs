using FootballChairman.Models;
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
        private readonly IManagerService _managerService;

        public GameServiceV2(IClubService clubService, IManagerService managerService)
        {
            _clubService = clubService;
            _managerService = managerService;
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
            //int homeScore;
            //int awayScore;
            //int homeScoreCompensation = 0;
            //int awayScoreCompensation = 0;
            //int highestSkill = Math.Max(_clubService.GetClub(fixture.HomeOpponentId).Skill, _clubService.GetClub(fixture.AwayOpponentId).Skill);
            //var competition = _competitionService.GetAllCompetitions().FirstOrDefault(com => com.Id == fixture.CompetitionId);
            //game.Fixture = fixture;

            //homeScore = RandomInt(0, 6) + _clubService.GetClub(fixture.HomeOpponentId).Skill - highestSkill;
            //awayScore = RandomInt(0, 6) + _clubService.GetClub(fixture.AwayOpponentId).Skill - highestSkill - 1;

            //if (homeScore < 0)
            //    awayScoreCompensation = 0 - homeScore;

            //if (awayScore < 0)
            //    homeScoreCompensation = 0 - awayScore;

            //game.HomeScore = homeScore + homeScoreCompensation;
            //game.AwayScore = awayScore + awayScoreCompensation;

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
