using BattleBase.Gameplay.Actors.Visual.Sound;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.View
{
    [RequireComponent(typeof(SoundEffectActivator))]
    public class SoundShotEffect : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField] private SoundEffect _prefab;

        private IAttackNotifier _attackNotifier;
        private SoundEffectActivator _activator;

        private void Awake()
        {
            _activator = GetComponent<SoundEffectActivator>();
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
            _activator.Activate(_prefab.Id);
    }
}