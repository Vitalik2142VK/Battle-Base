using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual
{
    public interface ITrailParticle
    {
        public string Id { get; }

        public void SetPosition(Vector3 position);

        public void Stop();
    }
}