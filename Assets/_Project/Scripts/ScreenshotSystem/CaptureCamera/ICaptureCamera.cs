using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public interface ICaptureCamera
    {
        public Camera Camera { get; }

        public LayerMask Layer { get; }

        public void SetCameraPosition(Vector3 position);

        public void CameraLookAt(Vector3 worldPosition);

        public void Show();

        public void Hide();
    }
}