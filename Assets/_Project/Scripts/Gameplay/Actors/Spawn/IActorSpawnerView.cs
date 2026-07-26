namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerView : IActorViewComponent
    {
        public void Init(IActorSpawnerNotifier events);
    }
}
