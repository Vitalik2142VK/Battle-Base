using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public interface ICameraSnapBack
    {
        public void Restore(Transform cameraTransform, float deltaTime);
    }
}