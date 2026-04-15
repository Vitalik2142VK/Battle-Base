namespace BattleBase.SaveService
{
    public interface IColorSaver : ISaver
    {
        public int PlayerColorIndex { get; }

        public int EnemyColorIndex { get; }

        public void SetPlayerColorIndex(int index);

        public void SetEnemyColorIndex(int index);
    }
}