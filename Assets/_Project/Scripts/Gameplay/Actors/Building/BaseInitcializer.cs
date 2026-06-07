using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Types;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BaseInitcializer : MonoBehaviour
    {
        [SerializeField] private ActorConfig _config;
        [SerializeField] private Base _playerBase;
        [SerializeField] private Base _enemyBase;

        private IComponentFactoryRegistry _componentFactoryRegistry;
        private IActorBinderRegistry _actorBinderRegistry;
        private IActorsController _actorsController;
        private IColorGetter _colorGetter;

        [Inject]
        public void Construct(
            IComponentFactoryRegistry componentFactoryRegistry,
            IActorBinderRegistry actorBinderRegistry,
            IActorsController actorsController,
            IColorGetter colorGetter)
        {
            _componentFactoryRegistry = componentFactoryRegistry ?? throw new ArgumentNullException(nameof(componentFactoryRegistry));
            _actorBinderRegistry = actorBinderRegistry ?? throw new ArgumentNullException(nameof(actorBinderRegistry));
            _actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            _colorGetter = colorGetter ?? throw new ArgumentNullException(nameof(colorGetter));
        }

        private void Start()
        {
            InitBase(_playerBase);
            InitBase(_enemyBase);
        }

        //todo get rid of the copy paste in to BuildingSiteInitcializer
        private void InitBase(Base baseView)
        {
            baseView.Init();

            ActorBuilder builder = new();
            builder
                .ActorView(baseView)
                .ActorData(_config.Data);

            IEnumerable<IComponentSource> componentSources = _config.GetComponentSources();

            foreach (var componentSource in componentSources)
            {
                IActorComponent component = _componentFactoryRegistry.Create(componentSource);
                builder.AddComponent(component);

                if (component is IDestroyableEvents damagebleEvents)
                    builder.DamagebleEvents(damagebleEvents);
            }

            Actor actor = builder.Build();
            _actorBinderRegistry.Bind(actor, baseView);
            _actorsController.AddActor(actor);

            TeamType team = baseView.Team;
            actor.Enable();
            actor.SetTeam(team);
            actor.ChangeColor(_colorGetter.GetTeamColor(team));
        }
    }
}