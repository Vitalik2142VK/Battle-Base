using BattleBase.Gameplay.Actors.AttackSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ShotEffect : MonoBehaviour, IAttackerViewComponent
    {
        private IAttackNotifier _attackNotifier;
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            if (_attackNotifier != null)
                _attackNotifier.Attacked += OnPlayShot;
        }

        private void OnDestroy()
        {
            if (_attackNotifier != null)
                _attackNotifier.Attacked -= OnPlayShot;
        }

        public void Init(IAttackNotifier attackNotifier)
        {
            _attackNotifier = attackNotifier ?? throw new System.ArgumentNullException(nameof(attackNotifier));

            if (gameObject.activeSelf)
                _attackNotifier.Attacked += OnPlayShot;
        }

        private void OnPlayShot() =>
            _particle.Play();
    }
}