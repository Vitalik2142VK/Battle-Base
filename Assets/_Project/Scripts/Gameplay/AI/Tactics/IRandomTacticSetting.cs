namespace BattleBase.Gameplay.AI.Tactics
{
    public interface IRandomTacticSetting : ITacticSetting
    {
        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}