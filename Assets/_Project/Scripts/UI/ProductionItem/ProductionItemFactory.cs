using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace BattleBase.UI
{
    public class ProductionItemFactory : IProductionItemFactory
    {
        private readonly ActorSpawnProductionItem _actorSpanwProductionItemPrefab;
        private readonly UpgradeProductionItem _upgradeProductionItemPrefab;
        private readonly DemolitionBuildingProductionItem _demolitionBuildingProductionItemPrefab;
        private readonly IObjectResolver _resolver;

        public ProductionItemFactory(
            ActorSpawnProductionItem actorSpanwProductionItemPrefab,
            UpgradeProductionItem upgradeProductionItemPrefab,
            DemolitionBuildingProductionItem demolitionBuildingProductionItemPrefab,
            IObjectResolver resolver)
        {
            _actorSpanwProductionItemPrefab = actorSpanwProductionItemPrefab != null ? actorSpanwProductionItemPrefab : throw new ArgumentNullException(nameof(actorSpanwProductionItemPrefab));
            _upgradeProductionItemPrefab = upgradeProductionItemPrefab != null ? upgradeProductionItemPrefab : throw new ArgumentNullException(nameof(upgradeProductionItemPrefab));
            _demolitionBuildingProductionItemPrefab = demolitionBuildingProductionItemPrefab != null ? demolitionBuildingProductionItemPrefab : throw new ArgumentNullException(nameof(demolitionBuildingProductionItemPrefab));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public List<IProductionItem> Create(IEnumerable<IProductionOption> productionOptions)
        {
            if (productionOptions == null)
                return new();

            List<IProductionItem> items = new();

            foreach (var productionOption in productionOptions)
            {
                IProductionItem item = _resolver.Instantiate(_actorSpanwProductionItemPrefab);
                item.SetInfo(productionOption);
                items.Add(item);
            }

            return items;
        }
    }
}