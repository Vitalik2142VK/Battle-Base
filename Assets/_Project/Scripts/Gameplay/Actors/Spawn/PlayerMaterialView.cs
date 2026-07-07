using BattleBase.Gameplay.Actors.Economy;
using TMPro;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class PlayerMaterialView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;

        private IMaterialData _data;

        private void OnEnable()
        {
            if (_data != null)
            {
                _data.DataChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnDisable()
        {
            if (_data != null)
                _data.DataChanged -= OnUpdateData;
        }

        [Inject]
        public void Construct(IMaterialRegistry materialRegistry)
        {
            if (materialRegistry == null)
                throw new System.ArgumentNullException(nameof(materialRegistry));

            _data = materialRegistry.GetMaterialData(TeamType.Player);

            if (gameObject.activeSelf)
            {
                _data.DataChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnUpdateData() =>
            _countText.text = _data.CurrentMaterials.ToString();
    }
}