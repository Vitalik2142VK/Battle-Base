using UnityEngine;

namespace BattleBase.Gameplay.Audio
{
    public interface IAudioListner
    {
        public bool CanListen(Vector3 position, float distance);
    }
}