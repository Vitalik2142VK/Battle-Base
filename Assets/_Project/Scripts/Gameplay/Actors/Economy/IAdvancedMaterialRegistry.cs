namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IAdvancedMaterialRegistry : IMaterialRegistry
    {
        public void AddMaterials(TeamType team, int materials);
    }
}