using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.Economy;
using System;
using System.Linq;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class SingleActorSpawnerFactory : IComponentFactory
    {
        private readonly IObjectResolver _resolver;

        private IActorSpawnService _spawnService;
        private IActorColorService _colorService;
        private IMaterialRegistry _materialRegistry;

        public SingleActorSpawnerFactory(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public Type SourceType => typeof(SingleActorSpawnerSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IActorSpawnerSource spawnComponentSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IActorSpawnerSource)}");

            _spawnService ??= _resolver.Resolve<IActorSpawnService>();
            _colorService ??= _resolver.Resolve<IActorColorService>();
            _materialRegistry ??= _resolver.Resolve<IMaterialRegistry>();

            var actorsToCreate = spawnComponentSource.ActorsConfigs.Select(a => a.Data);

            return new SingleActorSpawner(actorsToCreate, _spawnService, _colorService, _materialRegistry);
        }
    }
}