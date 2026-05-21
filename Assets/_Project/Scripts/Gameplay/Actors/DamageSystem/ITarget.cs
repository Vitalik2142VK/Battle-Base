using BattleBase.Gameplay.Actors.HealthSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface ITarget : ITargetPoint, IDamageble, ITeamable, IActorViewComponent
    {
        public void Init(IHealthPresenter healthPresenter, IDestroyableEvents damagebleEvents, ITeamable teamable);

        public bool HasHit(Vector3 hitPosition);
    }
}