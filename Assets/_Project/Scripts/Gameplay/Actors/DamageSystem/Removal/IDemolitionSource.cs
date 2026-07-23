namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public interface IDemolitionSource : IComponentSource
    {
        public IDemolitionData Data { get; }
    }
}