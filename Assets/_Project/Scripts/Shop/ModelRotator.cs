using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class ModelRotator : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;        
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        private void Update() =>
            Rotate(_direction, _speed * Time.deltaTime);

        public void Enable() =>
            enabled = true;

        public void Disable() =>
            enabled = false;

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);

        public void Rotate(Vector3 direction, float speed) =>
            _rotationTransform.eulerAngles += direction * speed;
    }
}