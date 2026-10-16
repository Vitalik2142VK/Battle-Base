using System;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private SpeedUpgradeBox _speedUpgradeBox;

        private ActorsUpgradeModel _actorsUpgradeModel;

        private IUpgradeInfo Info => _actorsUpgradeModel.PanelInfo.SpeedInfo;

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel)
        {
            _actorsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
        }

        private void OnEnable()
        {
            _actorsUpgradeModel.UnitSelectionChanged += UpdateInfo;
        }

        private void OnDisable()
        {
            _actorsUpgradeModel.UnitSelectionChanged -= UpdateInfo;
        }

        private void UpdateInfo()
        {
            if (Info.Levels.Count == 0)
                _speedUpgradeBox.gameObject.SetActive(false);
            else
                _speedUpgradeBox.gameObject.SetActive(true);
        }
    }
}