using BattleBase.Gameplay.Actors.Visual.Sound;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem.View
{
    public class ActorDestroySound : MonoBehaviour, IHealthViewComponent
    {
        [SerializeField] private SoundEffectActivator _activator;
        [SerializeField] private SoundEffect _prefab;
        [SerializeField][Range(0, 0.3f)] private float _pitchRange = 0f;

        private IHealthEvents _healthEvents;

        private void OnEnable()
        {
            if (_healthEvents != null)
                _healthEvents.Destroyed += OnPlay;
        }

        private void OnDisable()
        {
            if (_healthEvents != null)
                _healthEvents.Destroyed -= OnPlay;
        }

        public void Init(IHealthEvents healthEvents)
        {
            _healthEvents = healthEvents ?? throw new System.ArgumentNullException(nameof(healthEvents));

            if (gameObject.activeSelf)
                _healthEvents.Destroyed += OnPlay;
        }

        private void OnPlay() =>
            _activator.Activate(_prefab, _pitchRange);
    }
}