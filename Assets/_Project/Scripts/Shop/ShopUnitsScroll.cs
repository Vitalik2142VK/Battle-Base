using System.Collections.Generic;
using System.Linq;
using BattleBase.Utils.Extensions;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class ShopUnitsScroll : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ShopUnitItem _prefab;

        private readonly List<ShopUnitItem> _items = new();

        public ShopUnitItem CurrentItem { get; private set; }

        public void Init(IReadOnlyList<ShopUnitItemInfo> infos)
        {
            _content.ClearChilds();
            _items.Clear();

            foreach (ShopUnitItemInfo info in infos)
            {
                ShopUnitItem item = Instantiate(_prefab, _content);
                item.SetInfo(info);
                item.Unselect();
                _items.Add(item);
            }

            Select(_items.First());
        }

        public void UpdateInfo(ShopUnitItemInfo info)
        {
            foreach (ShopUnitItem item in _items)
            {
                if (item.UnitName == info.UnitName)
                    item.SetInfo(info);
            }
        }

        public void Select(ShopUnitItem item)
        {
            UnselectAll();
            item.Select();
            CurrentItem = item;
        }

        private void UnselectAll()
        {
            foreach (ShopUnitItem item in _items)
                item.Unselect();
        }
    }
}