using BattleBase.Gameplay.Actors.Visual.Particle;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(Projectile), typeof(ParticleActivator))]
    public class ParticleMissProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleView _prefab;
        [SerializeField] private float _size = 1f;

        private IProjectileMissEvent _event;
        private ParticleActivator _particleProjectile;

        private void Awake()
        {
            _event = GetComponent<IProjectileMissEvent>();
            _particleProjectile = GetComponent<ParticleActivator>();
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
            _particleProjectile.Activate(_prefab.Id, _size);
    }
}