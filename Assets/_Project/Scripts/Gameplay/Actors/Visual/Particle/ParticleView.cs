using BattleBase.Core;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleView : MonoBehaviour, IParticle, IPoolable<ParticleView>
    {
        private Transform _transform;
        private ParticleSystem _particle;

        public event Action<ParticleView> Deactivated;

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

        public void SetSize(float size) =>
            _transform.localScale = Vector3.one * size;

        public void Play() =>
            _particle.Play();
    }
}