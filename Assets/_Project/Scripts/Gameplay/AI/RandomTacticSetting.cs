using BattleBase.Gameplay.Actors;
using System;

namespace BattleBase.Gameplay.AI
{
    public class RandomTacticSetting
    {
        public RandomTacticSetting(TeamType team)
        {
            Team = team;
            MaxNumSpawn = 5;
            MinNumSpawn = 1;
        }

        public TeamType Team { get; }

        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}