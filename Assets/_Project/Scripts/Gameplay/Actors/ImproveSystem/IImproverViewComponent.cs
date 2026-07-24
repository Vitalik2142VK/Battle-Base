namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverViewComponent : IActorViewComponent
    {
        public void Init(IImproverINotifier improverNotifier);
    }
}