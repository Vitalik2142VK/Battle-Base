using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ShootEffect : MonoBehaviour, IAttackerViewComponent
    {
        private IAttackEvents _weaponEvents;
        private ParticleSystem _particle;

        private void Awake()
        {
            _particle = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            if (_weaponEvents != null)
                _weaponEvents.Attacked += OnPlayShot;
        }

        private void OnDestroy()
        {
            if (_weaponEvents != null)
                _weaponEvents.Attacked -= OnPlayShot;
        }

        public void Init(IAttackEvents weaponEvents)
        {
            _weaponEvents = weaponEvents ?? throw new System.ArgumentNullException(nameof(weaponEvents));

            if (gameObject.activeSelf)
                _weaponEvents.Attacked += OnPlayShot;
        }

        private void OnPlayShot() => _particle.Play();
    }
}