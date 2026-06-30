using BattleBase.Core;
using System;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorFactory : IFactory<Actor>
    {
        private readonly IActorConfig _config;
        private readonly IObjectResolver _resolver;
        private readonly IActorCreator _actorCreator;

        private int _unitCounter;

        public ActorFactory(IActorConfig config, IObjectResolver resolver, IActorCreator actorCreator)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _actorCreator = actorCreator ?? throw new ArgumentNullException(nameof(actorCreator));

            _unitCounter = 0;
        }

        public Actor Create()
        {
            ActorView prefab = _config.Data.Prefab;
            ActorView view = _resolver.Instantiate(prefab);

            view.name = $"{prefab.name}_{++_unitCounter}";

            Actor actor = _actorCreator.Create(view, _config);

            return actor;
        }
    }
}