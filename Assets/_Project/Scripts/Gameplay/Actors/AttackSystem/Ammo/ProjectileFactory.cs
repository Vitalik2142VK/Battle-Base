using BattleBase.Core;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileFactory : MonoBehaviour, IFactory<Projectile>
    {
        [SerializeField] private Projectile _misslePrefab;

        public string ProjectileId => _misslePrefab.Id;

        public Projectile Create()
        {
            return Instantiate(_misslePrefab);
        }
    }
}