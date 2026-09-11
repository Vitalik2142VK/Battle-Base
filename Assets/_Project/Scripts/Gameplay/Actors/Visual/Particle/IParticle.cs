using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public interface IParticle
    {
        public string Id { get; }

        public void SetPosition(Vector3 position);

        public void SetSize(float size);

        public void Play();
    }
}