using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class CaptrureCamera : MonoBehaviour, ICaptureCamera
    {
        [SerializeField] private Camera _camera;

        public Camera Camera => _camera;

        public LayerMask Layer => _camera.cullingMask;

        public void CameraLookAt(Vector3 worldPosition) =>
            _camera.transform.LookAt(worldPosition);

        public void SetCameraPosition(Vector3 worldPosition) =>
            _camera.transform.position = worldPosition;        

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);
    }
}