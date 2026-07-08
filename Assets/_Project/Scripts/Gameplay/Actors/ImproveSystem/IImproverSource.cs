namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverSource : IComponentSource
    {
        public IImproverData Data { get; }
    }
}