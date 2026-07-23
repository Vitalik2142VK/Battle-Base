namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialRegistry
    {
        public bool TryGetTransaction(TeamType team, int materials, out MatetialTransaction matetialTransaction);

        public bool TrySpend(TeamType team, int materials);

        public IMaterialData GetMaterialData(TeamType team);
    }
}