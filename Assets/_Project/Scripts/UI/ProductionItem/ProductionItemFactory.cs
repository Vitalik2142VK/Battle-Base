using System;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public class ProductionItemFactory : IProductionItemFactory
    {
        private readonly ProductionItem _itemPrefab;

        public ProductionItemFactory(ProductionItem itemPrefab)
        {
            _itemPrefab = itemPrefab != null ? itemPrefab : throw new ArgumentNullException(nameof(itemPrefab));
        }

        public List<IProductionItem> Create(IReadOnlyList<IProductionItemInfo> infos)
        {
            if (infos == null)
                return new();

            List<IProductionItem> items = new();

            foreach (IProductionItemInfo info in infos)
            {
                IProductionItem item = UnityEngine.Object.Instantiate(_itemPrefab);
                item.SetInfo(info);
                items.Add(item);
            }

            return items;
        }
    }
}