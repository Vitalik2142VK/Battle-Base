using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace BattleBase.UI
{
    public class ProductionItemFactory : IProductionItemFactory
    {
        private readonly ProductionItem _itemPrefab;
        private readonly IObjectResolver _resolver;

        public ProductionItemFactory(ProductionItem itemPrefab, IObjectResolver resolver)
        {
            _itemPrefab = itemPrefab != null ? itemPrefab : throw new ArgumentNullException(nameof(itemPrefab));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public List<IProductionItem> Create(IEnumerable<IProductionOption> productionOptions)
        {
            if (productionOptions == null)
                return new();

            List<IProductionItem> items = new();

            foreach (var productionOption in productionOptions)
            {
                IProductionItem item = _resolver.Instantiate(_itemPrefab);
                item.SetInfo(productionOption);
                items.Add(item);
            }

            return items;
        }
    }
}