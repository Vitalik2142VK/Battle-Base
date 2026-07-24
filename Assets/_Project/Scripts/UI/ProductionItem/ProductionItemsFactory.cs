using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public class ProductionItemsFactory : IProductionItemsFactory
    {
        private readonly IEnumerable<IProductionItemFactory> _factories;

        private readonly DemolitionBuildingProductionItem _demolitionBuildingProductionItemPrefab;

        public ProductionItemsFactory(IEnumerable<IProductionItemFactory> factories,
            DemolitionBuildingProductionItem demolitionBuildingProductionItemPrefab)
        {
            _factories = factories ?? throw new ArgumentNullException(nameof(factories));

            _demolitionBuildingProductionItemPrefab = demolitionBuildingProductionItemPrefab != null ? demolitionBuildingProductionItemPrefab : throw new ArgumentNullException(nameof(demolitionBuildingProductionItemPrefab));
        }

        public List<IProductionItem> Create(IEnumerable<IProductionOption> productionOptions)
        {
            if (productionOptions == null)
                return new();

            List<IProductionItem> items = new();

            foreach (var productionOption in productionOptions)
            {
                foreach (var factory  in _factories)
                {
                    if (factory.TryCreate(productionOption, out IProductionItem item))
                        items.Add(item);
                }
            }

            return items;
        }
    }
}