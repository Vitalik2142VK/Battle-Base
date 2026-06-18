using BattleBase.Core;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual
{
    [RequireComponent(typeof(ParticleSystem))]
    public class TrailParticle : MonoBehaviour, ITrailParticle, IPoolable<TrailParticle>
    {
        private Transform _transform;
        private ParticleSystem _particle;

        public event Action<TrailParticle> Deactivated;

        public string Id => gameObject.name;

        private void Awake()
        {
            _transform = transform;

            _particle = GetComponent<ParticleSystem>();
            var main = _particle.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Deactivated?.Invoke(this);
        }

        public void SetPosition(Vector3 position) =>
            _transform.position = position;

        public void Stop() =>
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}