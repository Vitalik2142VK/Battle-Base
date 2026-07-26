namespace BattleBase.Gameplay.AI.TacticTypes
{
    public interface IRandomTacticSetting : ITacticSetting
    {
        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}