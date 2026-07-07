using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class SingleActorSpawner : ActorSpawner
    {
        private readonly IActorSpawnService _spawnService;
        private readonly IActorColorService _colorService;
        
        private readonly Timer _timer;

        private IActorData _currentActorData;
        private bool _isDisable;

        public override event Action<IActor> Spawned;

        public SingleActorSpawner(
            IEnumerable<IActorData> actorsToCreate,
            IActorSpawnService actorSpawnService,
            IActorColorService colorService,
            IMaterialRegistry materialRegistry) : base(actorsToCreate, materialRegistry)
        {
            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
            _timer = new();
        }

        public override void Enable()
        {
            _isDisable = false;
        }

        public override void Disable()
        {
            _isDisable = true;
            _currentActorData = null;
        }

        public override void Update(float delta)
        {
            if (_currentActorData == null || _isDisable)
                return;

            if (IsInProcessSpawn == false)
            {
                if (CanBeginSpawn(_currentActorData) == false)
                    return;
            }

            if (_timer.IsTimeUp == false)
            {
                _timer.Tick(delta);

                return;
            }

            ProcessSpawn();
        }

        public override void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (ConstrainActorData(actorData))
            {
                _currentActorData = actorData;
                _timer.SetWaitTime(_currentActorData.ConstructionTime);
                _timer.RestartTimer();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(actorData)} not found");
            }
        }

        private void ProcessSpawn()
        {
            Actor actor = _spawnService.Spawn(_currentActorData.Id, Teamable.TeamType, SpawnData);
            actor.Enable();

            Spawned?.Invoke(actor); //todo check this event

            _colorService.EstabilshColor(actor, actor.View);
            _currentActorData = null;

            FinishSpawn();
        }
    }
}