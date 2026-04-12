using Cinemachine;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 1f;
        [SerializeField] private float minZoom = 0.5f;
        [SerializeField] private float maxZoom = 1.5f;

        private CinemachineVirtualCamera _virtualCamera;

        private void Awake() =>
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        private void Update()
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput != 0)
            {
                float newSize = _virtualCamera.m_Lens.OrthographicSize - scrollInput * zoomSpeed;
                _virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            }
        }
    }
}