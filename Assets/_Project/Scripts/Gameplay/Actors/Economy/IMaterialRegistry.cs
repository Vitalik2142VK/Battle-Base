namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialRegistry
    {
        public bool TrySpend(TeamType team, int materials);

        public IMaterialData GetMaterialData(TeamType team);
    }
}