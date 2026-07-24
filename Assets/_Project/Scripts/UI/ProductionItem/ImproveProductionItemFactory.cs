using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Production.Improve;
using System;
using VContainer;
using VContainer.Unity;

namespace BattleBase.UI
{
    public class ImproveProductionItemFactory : IProductionItemFactory
    {
        private readonly ImproveProductionItem _prefab;
        private readonly IObjectResolver _resolver;

        public ImproveProductionItemFactory(ImproveProductionItem prefab, IObjectResolver resolver)
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

            if (option.Type != TypeProduction.Improve || 
                option is IImproveProductionOption improveProductionOption == false)
            {
                return false;
            }

            ImproveProductionItem actorSpawnProductionItem = _resolver.Instantiate(_prefab);
            ProductionOptionPresenter presenter = new(improveProductionOption);
            actorSpawnProductionItem.Init(
                presenter, 
                improveProductionOption.Data,
                improveProductionOption.MaterialData,
                improveProductionOption.ImproverState);
            item = actorSpawnProductionItem;

            return true;
        }
    }
}