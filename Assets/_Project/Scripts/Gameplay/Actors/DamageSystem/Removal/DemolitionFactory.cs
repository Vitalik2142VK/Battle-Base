using BattleBase.Gameplay.Actors.Economy;
using System;
using VContainer;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class DemolitionFactory : IComponentFactory
    {
        private readonly IObjectResolver _resolver;

        private IAdvancedMaterialRegistry _materialRegistry;

        public DemolitionFactory(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public Type SourceType => typeof(DemolitionSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IDemolitionSource demolitionSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IDemolitionSource)}");

            _materialRegistry ??= _resolver.Resolve<IAdvancedMaterialRegistry>();

            return new Demolition(demolitionSource.Data, _materialRegistry);
        }
    }
}