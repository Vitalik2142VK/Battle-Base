namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreatorView : IActorViewComponent
    {
        public void Init(IMaterialCreatorEvents events);
    }
}