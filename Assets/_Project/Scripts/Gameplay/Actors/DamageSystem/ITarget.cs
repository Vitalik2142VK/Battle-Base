using BattleBase.Gameplay.Actors.HealthSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface ITarget : IDamageble, IActorViewComponent
    {
        public void Init(IHealthPresenter healthPresenter, IDamagebleEvents damagebleEvents);

        public Vector3 Position { get; }

        public void TryHit(Vector3 hitPosition);
    }
}