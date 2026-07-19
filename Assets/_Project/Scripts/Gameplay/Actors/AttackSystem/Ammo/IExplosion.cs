using BattleBase.Gameplay.Actors.DamageSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IExplosion
    {
        public void Explode(IDamage damage, Vector3 positionExposion);
    }
}