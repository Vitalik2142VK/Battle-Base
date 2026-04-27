using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public interface ICameraSnapBack
    {
        public void Restore(Transform cameraTransform, float deltaTime);
    }
}