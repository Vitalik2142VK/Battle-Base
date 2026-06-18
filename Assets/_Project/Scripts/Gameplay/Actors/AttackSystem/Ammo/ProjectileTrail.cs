using BattleBase.Gameplay.Actors.Visual;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileTrail : MonoBehaviour
    {
        [SerializeField][SerializeIterface(typeof(ITrailParticle))] private GameObject _prefabTrailParticle;

        private ITrailParticleSpawner _spawner;
        private ITrailParticle _trailParticle;
        private Transform _transform;
        private string _trailParticleId;

        private void Awake()
        {
            _transform = transform;
            _trailParticleId = _prefabTrailParticle.GetComponent<ITrailParticle>().Id;
        }

        private void OnEnable()
        {
            _trailParticle = _spawner.Spawn(_trailParticleId);
        }

        private void FixedUpdate()
        {
            _trailParticle.SetPosition(_transform.position);
            _trailParticle.SetRotation(_transform.rotation);
        }

        private void OnDisable()
        {
            if (_trailParticle.IsActive)
                _trailParticle.Stop();
        }

        [Inject]
        public void Construct(ITrailParticleSpawner spawner)
        {
            _spawner = spawner ?? throw new System.ArgumentNullException(nameof(spawner));
        }
    }
}