using BattleBase.Gameplay.MiniMap;

namespace BattleBase.Gameplay.Actors.Colored
{
    public interface IColoredActorView : IActorViewComponent
    {
        public ITrackable Trackable { get; }

        public void Init(IColored colored);
    }
}
