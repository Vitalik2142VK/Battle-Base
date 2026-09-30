using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectActivator : MonoBehaviour
    {
        [SerializeField] private SoundEffect _prefab;
        [SerializeField] private Transform _spawnPoint;

        private ISoundEffectSpawner _spawner;
        private string _particleId;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        private void Awake()
        {
            _particleId = _prefab.Id;
        }

        public void Activate()
        {
            ISoundEffect soundEffect = _spawner.Spawn(_particleId);
            soundEffect.SetPosition(_spawnPoint.position);
            soundEffect.Play();
        }

        [Inject]
        public void Construct(ISoundEffectSpawner spawner)
        {
            _spawner = spawner ?? throw new System.ArgumentNullException(nameof(spawner));
        }
    }
}