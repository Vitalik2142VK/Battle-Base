using System;
using BattleBase.DI;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryStatusIndicatorFactory : IInjectable
    {
        private readonly TerritoryStatusIndicator _prefab;
        private readonly IObjectResolver _resolver;

        public TerritoryStatusIndicatorFactory(TerritoryStatusIndicator prefab, IObjectResolver resolver)
        {
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public TerritoryStatusIndicator Create() =>
            _resolver.Instantiate(_prefab);
    }
}