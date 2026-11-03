using BattleBase.Gameplay.Actors.Visual.Sound;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(Projectile), typeof(SoundEffectActivator))]
    public class SoundMissProjectile : MonoBehaviour
    {
        [SerializeField] private SoundEffect _prefab;

        private IProjectileMissEvent _event;
        private SoundEffectActivator _activator;

        private void Awake()
        {
            _event = GetComponent<IProjectileMissEvent>();
            _activator = GetComponent<SoundEffectActivator>();
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
            _activator.Activate(_prefab);
    }
}