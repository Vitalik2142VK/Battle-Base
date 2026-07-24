using BattleBase.Gameplay.Actors.Production;
using System;
using VContainer;
using VContainer.Unity;

namespace BattleBase.UI
{
    public class DemolitionBuildingProductionItemFactory : IProductionItemFactory
    {
        private readonly DemolitionBuildingProductionItem _prefab;
        private readonly IObjectResolver _resolver;

        public DemolitionBuildingProductionItemFactory(
            DemolitionBuildingProductionItem prefab, 
            IObjectResolver resolver)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            _prefab = prefab;
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public bool TryCreate(IProductionOption option, out IProductionItem item)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            item = null;

            if (option.Type != TypeProduction.Removal)
                return false;

            DemolitionBuildingProductionItem demolitionBuildingProductionItem = _resolver.Instantiate(_prefab);
            ProductionOptionPresenter presenter = new(option);
            demolitionBuildingProductionItem.Init(presenter, option.Data);
            item = demolitionBuildingProductionItem;

            return true;
        }
    }
}