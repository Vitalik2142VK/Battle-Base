using BattleBase.Gameplay.Actors.HealthSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface ITarget : ITargetPoint, IDamageble, IActorViewComponent
    {
        public void Init(IHealthPresenter healthPresenter, IDamagebleEvents damagebleEvents);

        public bool HasHit(Vector3 hitPosition);
    }
}