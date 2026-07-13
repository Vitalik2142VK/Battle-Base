using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class ModelRotator : MonoBehaviour
    {
        [SerializeField] private Transform _rotationTransform;        
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        private void Update() =>
            _rotationTransform.eulerAngles += _speed * Time.deltaTime * _direction;
    }
}