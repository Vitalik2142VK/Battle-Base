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

        //private IComponentFactoryRegistry _componentFactoryRegistry;
        //private IActorBinderRegistry _actorBinderRegistry;
        //private IActorsController _actorsController;
        //private IActorColorService _colorService;
        private IActorComposer _composer;

        [Inject]
        public void Construct(
            //IComponentFactoryRegistry componentFactoryRegistry,
            //IActorBinderRegistry actorBinderRegistry,
            //IActorsController actorsController,
            //IActorColorService colorService,
            IActorComposer composer)
        {
            //_componentFactoryRegistry = componentFactoryRegistry ?? throw new ArgumentNullException(nameof(componentFactoryRegistry));
            //_actorBinderRegistry = actorBinderRegistry ?? throw new ArgumentNullException(nameof(actorBinderRegistry));
            //_actorsController = actorsController ?? throw new ArgumentNullException(nameof(actorsController));
            //_colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
            _composer = composer ?? throw new ArgumentNullException(nameof(composer));
        }

        private void Start()
        {
            InitBase(_playerBase);
            InitBase(_enemyBase);
        }

        private void InitBase(Base view) => 
            _composer.Compose(view, _config, view.Team);
    }
}