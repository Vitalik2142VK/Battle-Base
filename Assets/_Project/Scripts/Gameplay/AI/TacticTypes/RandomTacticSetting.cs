namespace BattleBase.Gameplay.AI.TacticTypes
{
    public class RandomTacticSetting
    {
        public RandomTacticSetting()
        {
            MaxNumSpawn = 3;
            MinNumSpawn = 1;
        }

        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}