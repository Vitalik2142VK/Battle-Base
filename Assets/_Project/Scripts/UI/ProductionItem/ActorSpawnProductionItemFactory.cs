using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Production.Spawn;
using System;
using VContainer;
using VContainer.Unity;

namespace BattleBase.UI
{
    public class ActorSpawnProductionItemFactory : IProductionItemFactory
    {
        private readonly ActorSpawnProductionItem _prefab;
        private readonly IObjectResolver _resolver;

        public ActorSpawnProductionItemFactory(ActorSpawnProductionItem prefab, IObjectResolver resolver)
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

            if (option.Type != TypeProduction.Spawn || option is ISpawnProductionOption spawnOption == false)
                return false;

            ActorSpawnProductionItem actorSpawnProductionItem = _resolver.Instantiate(_prefab);
            SpawnProductionOptionPresenter presenter = new(spawnOption);
            actorSpawnProductionItem.Init(presenter, spawnOption.SpawnData, spawnOption.Data);
            item = actorSpawnProductionItem;

            return true;
        }
    }
}