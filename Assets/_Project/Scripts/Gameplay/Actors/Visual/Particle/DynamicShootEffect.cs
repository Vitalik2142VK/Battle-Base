using BattleBase.Gameplay.Actors.AttackSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class DynamicShootEffect : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField] private ParticleSystem[] _shotEffects;

        private IAttackNotifier _attackNotifier;
        private int _currentIndexPoint = 0;

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

        private void OnPlayShot()
        {
            if (_currentIndexPoint >= _shotEffects.Length)
                _currentIndexPoint = 0;

            _shotEffects[_currentIndexPoint++].Play();
        }
    }
}