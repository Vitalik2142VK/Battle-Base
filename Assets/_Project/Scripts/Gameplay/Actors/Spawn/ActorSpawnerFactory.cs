using System;
using System.Linq;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerFactory : IComponentFactory
    {
        private readonly IObjectResolver _resolver;

        private IActorSpawnService _spawnService;

        public ActorSpawnerFactory(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public Type SourceType => typeof(SpawnComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is ISpawnComponentSource spawnComponentSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(ISpawnComponentSource)}");

            _spawnService ??= _resolver.Resolve<IActorSpawnService>();

            var actorsToCreate = spawnComponentSource.ActorsConfigs.Select(a => a.Data);

            return new ActorSpawner(actorsToCreate, _spawnService);
        }
    }
}