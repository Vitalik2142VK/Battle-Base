using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Commands;
using BattleBase.Utils.Extensions;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopUnitsScroll : MonoBehaviour
    {
        [SerializeField] private CommandRebuildLayout _commandRebuildLayout;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopUnitItemView _prefab;

        private readonly List<ShopUnitItemView> _items = new();

        public ShopUnitItemView CurrentItem { get; private set; }

        private UnitsUpgradeModel _unitsUpgradeModel;

        [Inject]
        public void Construct(UnitsUpgradeModel unitsUpgradeModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));

            Init(unitsUpgradeModel.Infos);
        }

        public void Init(IReadOnlyList<IShopUnitItemInfo> infos)
        {
            _content.ClearChilds();
            _items.Clear();

            foreach (IShopUnitItemInfo info in infos)
            {
                ShopUnitItemView item = Instantiate(_prefab, _content);
                item.SetInfo(info, Select);
                item.Unselect();
                _items.Add(item);
            }

            Select(_items.First());
        }

        public void Select(ShopUnitItemView item)
        {
            UnselectAll();
            item.Select();
            CurrentItem = item;

            _unitsUpgradeModel.SelectUnit(item.Info);

            _commandRebuildLayout.Execute();
        }

        private void UnselectAll()
        {
            foreach (ShopUnitItemView item in _items)
                item.Unselect();
        }
    }
}