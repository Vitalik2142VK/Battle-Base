namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreatorSource : IComponentSource
    {
        public IMaterialCreatorConfig Config { get; }
    }
}