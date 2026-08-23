using BattleBase.Gameplay.Actors.Movement.Air;

namespace BattleBase.Gameplay.Actors.Movement.Jet
{
    public interface IJetMoveComponentSource : IAirMoveComponentSource
    {
        public float OffsetUturn {  get; }
    }
}
