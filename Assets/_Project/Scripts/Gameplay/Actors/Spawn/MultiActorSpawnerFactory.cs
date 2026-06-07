using BattleBase.Gameplay.Actors.Colored;
using System;
using System.Linq;
using VContainer;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class MultiActorSpawnerFactory : IComponentFactory
    {
        private readonly IObjectResolver _resolver;

        private IActorSpawnService _spawnService;
        private IColorGetter _colorGetter;

        public MultiActorSpawnerFactory(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public Type SourceType => typeof(MultiSpawnComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is ISpawnComponentSource spawnComponentSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(ISpawnComponentSource)}");

            _spawnService ??= _resolver.Resolve<IActorSpawnService>();
            _colorGetter ??= _resolver.Resolve<IColorGetter>();

            var actorsToCreate = spawnComponentSource.ActorsConfigs.Select(a => a.Data);

            return new MultiActorSpawner(actorsToCreate, _spawnService, _colorGetter);
        }
    }
}