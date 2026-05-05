using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Spawn
{
    [RequireComponent(typeof(Renderer))]
    public class ActorFactory : MonoBehaviour, IFactory<Actor>
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private TeamType _team;

        private Renderer _renderer;
        private int _spawnedUnits;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _spawnedUnits = 0;
        }

        public Actor Create()
        {
            ActorView prefab = _config.Prefab;
            string name = $"{_team}_{prefab.name}_{++_spawnedUnits}";

            ActorView view = Instantiate(prefab, transform);
            view.gameObject.name = name;
            view.Init();

            ActorDataModifier actorDataModified = new(_config.Data, _team);
            ActorBuilder builder = new();
            builder
                .ActorView(view)
                .ActorData(actorDataModified);

            IEnumerable<IComponentSource> componentSources = _config.GetComponentSources();

            foreach (var componentSource in componentSources)
            {
                IActorComponent component = componentSource.Create();
                builder.AddComponent(component);

                if (component is IDamagebleEvents damagebleEvents)
                    builder.DamagebleEvents(damagebleEvents);
            }

            Actor actor = builder.Build();

            //todo Add to Container 
            ActorBinderRegistry actorBinderRegistry = new(new List<IActorComponentBinder>()
            {
                new HealthBinder()
            });

            actorBinderRegistry.Bind(actor, view);

            //todo Delete after the selected color is implemented ...
            var renderersActor = GetComponentsInChildren<MeshRenderer>(true);

            foreach (var renderer in renderersActor)
                renderer.material = _renderer.material;
            //...

            return actor;
        }
    }
}