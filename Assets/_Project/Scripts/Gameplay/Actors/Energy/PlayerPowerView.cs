using TMPro;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PlayerPowerView : MonoBehaviour
    {
        private const string Format = "{0}/{1}";

        [SerializeField] private TMP_Text _countText;

        private IPowerData _data;

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
        public void Construct(IPowerRegistry powerRegistry)
        {
            if (powerRegistry == null)
                throw new System.ArgumentNullException(nameof(powerRegistry));

            _data = powerRegistry.GetPowerEvent(TeamType.Player);

            if (gameObject.activeSelf)
            {
                _data.DataChanged += OnUpdateData;

                OnUpdateData();
            }
        }

        private void OnUpdateData() => 
            _countText.text = string.Format(Format, _data.UsedEnergy, _data.CurrentCapacity);
    }
}
