namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerGeneratorView : IActorViewComponent
    {
        public void Init(IPowerGeneratorNotifier notifier);
    }
}
