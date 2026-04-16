using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraBoundsLimiter
    {
        public bool IsValidPositionX(Vector3 position);

        public bool IsValidPositionZ(Vector3 position);

        public float GetOvershootX(Vector3 position);

        public float GetOvershootZ(Vector3 position);
    }
}