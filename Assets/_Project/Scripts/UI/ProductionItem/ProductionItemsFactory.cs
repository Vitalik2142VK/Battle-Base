using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.UI
{
    public class ProductionItemsFactory : IProductionItemsFactory
    {
        private readonly IEnumerable<IProductionItemFactory> _factories;

        public ProductionItemsFactory(IEnumerable<IProductionItemFactory> factories)
        {
            _factories = factories ?? throw new ArgumentNullException(nameof(factories));
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