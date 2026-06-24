using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Shop
{
    public class ShopUpgradePanel : MonoBehaviour
    {
        [SerializeField] private ShopUpgradeButton _damageButton;
        [SerializeField] private ShopUpgradeButton _armorButton;
        [SerializeField] private ShopUpgradeButton _buildTimeButton;
        [SerializeField] private Image _preview;

        public void SetInfo(ShopUpgradePanelInfo info)
        {
            _damageButton.SetInfo(info.DamageInfo);
            _armorButton.SetInfo(info.ArmorInfo);
            _buildTimeButton.SetInfo(info.BuildTimeInfo);
            _preview.sprite = info.Preview;
        }
    }
}