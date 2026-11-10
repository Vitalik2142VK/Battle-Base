using BattleBase.ShopSystem;
using UnityEngine;

namespace BattleBase.Utils
{
    public class NeededGoldCounter : MonoBehaviour
    {
        [SerializeField] private ActorsUpgradeConfig _config;

        private void OnValidate()
        {
            if (_config == null)
                throw new System.NullReferenceException(nameof(_config));
        }

        [ContextMenu(nameof(Count))]
        public void Count()
        {
            long goldCount = 0;

            foreach (var info in _config.Infos)
                goldCount += CalculateUpgradeActor(info.PanelInfo);

            Debug.Log($"Number gold for full upgrade = '{goldCount}'");
        }

        private int CalculateUpgradeActor(IUpgradeStatsInfo statsInfo)
        {
            int result = 0;

            result += CalculateUpgradeInfo(statsInfo.HealthInfo);
            result += CalculateUpgradeInfo(statsInfo.DamageInfo);
            result += CalculateUpgradeInfo(statsInfo.SpeedInfo);

            return result;
        }

        private int CalculateUpgradeInfo(IUpgradeInfo upgradeInfo)
        {
            int result = 0;

            foreach (var price in upgradeInfo.Levels)
                result += price;

            return result;
        }
    }
}