using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual
{
    public class UiLookToCamera : MonoBehaviour
    {
        private Transform _cameraTransform;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            float cameraXRotation = _cameraTransform.eulerAngles.x;
            float cameraYRotation = _cameraTransform.eulerAngles.y;

            _transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0);
        }
    }
}