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
            base.Enable();

            _isDisable = false;
        }

        public override void Disable()
        {
            base.Disable();

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

            _timer.Tick(delta);

            if (_timer.IsTimeUp == false)
            {
                CalcualteProcessSpawn(delta);

                return;
            }

            FinishSpawn();
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

                AddActorToSpawnData(actorData);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(actorData)} not found");
            }
        }

        public override void CancelSpawnActor(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_currentActorData == actorData)
            {
                CancelSpawn();
                RemoveActorToSpawnData(_currentActorData);
                _currentActorData = null;
            }
        }

        protected override void Spawn()
        {
            Actor actor = _spawnService.Spawn(_currentActorData.Id, Teamable.TeamType, SpawnData);
            actor.Enable();

            Spawned?.Invoke(actor);

            _colorService.EstabilshColor(actor, actor.View);
            _currentActorData = null;
        }
    }
}