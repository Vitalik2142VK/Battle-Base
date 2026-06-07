namespace BattleBase.Gameplay.Actors 
{
    public interface IColoredActorView : IActorViewComponent
    {
        public void Init(IColored colored);
    }
}
