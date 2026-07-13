using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class RenderingModelInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _modelParent;

        private GameObject _currentModel;

        public void SetModel(GameObject model)
        {
            if (_currentModel != null)
                Destroy(_currentModel);

            _currentModel = model;
            _currentModel.transform.SetParent(_modelParent);
            _currentModel.transform.SetPositionAndRotation(_modelParent.position, _modelParent.rotation);
        }
    }
}