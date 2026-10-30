using BattleBase.Gameplay.Actors.Visual.Sound;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(Projectile), typeof(SoundEffectActivator))]
    public class SoundHitProjectile : MonoBehaviour
    {
        [SerializeField] private SoundEffect _prefab;

        private IProjectileHitEvent _event;
        private SoundEffectActivator _activator;

        private void Awake()
        {
            _event = GetComponent<IProjectileHitEvent>();
            _activator = GetComponent<SoundEffectActivator>();
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
            _activator.Activate(_prefab);
    }
}