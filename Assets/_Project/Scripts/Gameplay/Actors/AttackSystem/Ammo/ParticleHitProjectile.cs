using BattleBase.Gameplay.Actors.Visual.Particle;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(Projectile))]
    public class ParticleHitProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleProjectile _particleProjectile;

        private IProjectileHitEvent _event;

        private void Awake()
        {
            _event = GetComponent<IProjectileHitEvent>();
        }

        private void OnEnable()
        {
            _event.Hited += OnPlay;
        }

        private void OnDisable()
        {
            _event.Hited -= OnPlay;
        }

        private void OnPlay() => 
            _particleProjectile.Play();
    }
}