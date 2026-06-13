using BattleBase.Gameplay.Actors.HealthSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface ITarget : ITargetPoint, IDamageble, ITeamable, IActorViewComponent
    {
        public ActorMask ActorMask { get; }

        public void Init(
            IHealthPresenter healthPresenter, 
            IDestroyableEvents damagebleEvents, 
            ITeamable teamable, 
            ActorMask actorMask);

        public bool HasHit(Vector3 hitPosition);
    }
}