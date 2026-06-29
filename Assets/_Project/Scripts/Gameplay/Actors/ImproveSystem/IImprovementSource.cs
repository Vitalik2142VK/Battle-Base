namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImprovementSource : IComponentSource
    {
        public IImprovementData Data { get; }
    }
}