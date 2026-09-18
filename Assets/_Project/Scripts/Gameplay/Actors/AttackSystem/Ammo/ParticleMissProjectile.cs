using BattleBase.Gameplay.Actors.Visual.Particle;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(Projectile))]
    public class ParticleMissProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleProjectile _particleProjectile;

        private IProjectileMissEvent _event;

        private void Awake()
        {
            _event = GetComponent<IProjectileMissEvent>();
        }

        private void OnEnable()
        {
            _event.Missed += OnPlay;
        }

        private void OnDisable()
        {
            _event.Missed -= OnPlay;
        }

        private void OnPlay() =>
            _particleProjectile.Play();
    }
}