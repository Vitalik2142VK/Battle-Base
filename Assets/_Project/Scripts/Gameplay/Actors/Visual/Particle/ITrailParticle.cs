using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public interface ITrailParticle
    {
        public string Id { get; }

        public bool IsActive { get; }

        public void SetPosition(Vector3 position);

        public void SetRotation(Quaternion rotation);

        public void Stop();
    }
}