using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public interface IHuntMover : IMover, IUpdateable
    {
        public void Init(IActorPosition actorPosition);

        public void EstablishTarget(ITarget target);

        public void ResetTarget();
    }
}
