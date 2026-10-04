using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class RenderingModelInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _modelParent;

        private GameObject _currentModel;

        public bool HasModel => _currentModel != null;

        public void SetModel(GameObject model, Vector3 offset)
        {
            if (model == null)
                return;

            if (_currentModel == model)
                return;

            if (gameObject.activeSelf == false)
                gameObject.SetActive(true);

            if (_currentModel != null)
                Destroy(_currentModel);

            _currentModel = model;
            _currentModel.transform.SetParent(_modelParent);
            _currentModel.transform.SetPositionAndRotation(_modelParent.position + offset, _modelParent.rotation);
        }

        public void Clear()
        {
            if (_currentModel != null)
                Destroy(_currentModel);

            _currentModel = null;
        }
    }
}