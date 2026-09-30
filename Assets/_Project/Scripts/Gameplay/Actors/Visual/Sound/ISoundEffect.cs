using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public interface ISoundEffect
    {
        public string Id { get; }

        public void SetPosition(Vector3 position);

        public void Play();
    }
}