using System;
using System.Collections.Generic;
using BattleBase.Commands;
using BattleBase.DI;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class Shop : MonoBehaviour, IInjectable
    {
        [SerializeField] private CommandRebuildLayout _commandRebuildLayout;
        [SerializeField] private ShopUnitsScroll _scroll;
        [SerializeField] private ShopUpgradePanel _panel;

        // todo эти данные придут из конфигов, сейчас тестово тут
        [SerializeField] private List<ShopUnitItemInfo> _infos;

        private CreditsModel _credits;

        [Inject]
        public void Construct(CreditsModel credits)
        {
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));
        }

        private void Awake()
        {
            foreach (ShopUnitItemInfo info in _infos)
            {
                info.Clicked = OnClickItem;
                info.PanelInfo.DamageInfo.Clicked = OnUpgradeClicked;
                info.PanelInfo.ArmorInfo.Clicked = OnUpgradeClicked;
                info.PanelInfo.BuildTimeInfo.Clicked = OnUpgradeClicked;
            }

            _scroll.Init(_infos);

            OnClickItem(_scroll.CurrentItem);
        }

        private void OnClickItem(ShopUnitItem item)
        {
            _scroll.Select(item);
            _panel.SetInfo(item.PanelInfo, item.Info.Preview);
            _commandRebuildLayout.Execute();
        }

        private void OnUpgradeClicked(ShopUpgradeButton button)
        {
            UpgradeButtonInfo upgradeInfo = button.Info;

            if (_credits.TrySpend(button.Price))
            {
                upgradeInfo.CurrentLevel++;
                _scroll.UpdateInfo(_scroll.CurrentItem.Info);
                _panel.UpdateInfo();

                _commandRebuildLayout.Execute();
            }            
        }
    }
}