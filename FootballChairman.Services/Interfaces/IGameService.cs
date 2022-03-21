﻿using FootballChairman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Services.Interfaces
{
    public interface IGameService
    {
        Game PlayGame(Fixture fixture);
        IList<Game> GetGames();

    }
}
