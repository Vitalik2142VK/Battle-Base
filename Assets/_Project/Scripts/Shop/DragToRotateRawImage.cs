using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleBase.ShopSystem
{
    public class DragToRotateRawImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private ModelRotator _modelRotator;
        [SerializeField] private float _dragSensitivity = 0.2f;

        private void OnEnable() =>
            _modelRotator.Show();

        private void OnDisable() =>
            _modelRotator.Hide();

        public void OnPointerDown(PointerEventData eventData) =>
            _modelRotator.Disable();

        public void OnDrag(PointerEventData eventData)
        {
            float deltaX = eventData.delta.x;
            _modelRotator.Rotate(Vector3.up, -deltaX * _dragSensitivity);
        }

        public void OnPointerUp(PointerEventData eventData) =>
            _modelRotator.Enable();
    }
}