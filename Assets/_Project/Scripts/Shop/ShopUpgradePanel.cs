using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.ShopSystem
{
    public class ShopUpgradePanel : MonoBehaviour
    {
        [SerializeField] private DamageShopUpgradeButton _damageButton;
        [SerializeField] private ArmorShopUpgradeButton _armorButton;
        [SerializeField] private BuildTimeShopUpgradeButton _buildTimeButton;
        [SerializeField] private Image _preview;

        public ShopUpgradePanelInfo Info { get; internal set; }

        public void SetInfo(ShopUpgradePanelInfo info, Sprite preview)
        {
            _preview.sprite = preview;
            Info = info;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            _damageButton.SetInfo(Info.DamageInfo);
            _armorButton.SetInfo(Info.ArmorInfo);
            _buildTimeButton.SetInfo(Info.BuildTimeInfo);
        }
    }
}