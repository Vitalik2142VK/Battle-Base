using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class MultiActorSpawner : ActorSpawner
    {
        private readonly List<IActorData> _actorsQueue;
        private readonly IActorSpawnService _spawnService;
        private readonly IActorColorService _colorService;
        private readonly IPowerRegistry _powerRegistry;
        private readonly Timer _timer;

        private IActorData _currentActorData;
        private bool _isDisable;

        public override event Action<IActor> Spawned;
        public override event Action SpawnStarted;
        public override event Action SpawnCancled;
        public override event Action SpawnFinished;

        public MultiActorSpawner(
            IEnumerable<IActorData> actorsToCreate,
            IActorSpawnService actorSpawnService,
            IActorColorService colorService,
            IMaterialRegistry materialRegistry,
            IPowerRegistry powerRegistry) : base(actorsToCreate, materialRegistry)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsQueue = new List<IActorData>();

            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
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

            if (_powerRegistry.TryReserve(Teamable.TeamType, _currentActorData.Power))
            {
                FinishSpawn();

                SpawnFinished?.Invoke();
            }
        }

        public override void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (ConstrainActorData(actorData))
            {
                if (_currentActorData == null)
                    EstablisCurrentActorSpawn(actorData);
                else
                    _actorsQueue.Add(actorData);

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
                _currentActorData = null;

                SpawnCancled?.Invoke();
            }
            else
            {
                RemoveActorDataFromQueue(actorData);
                RemoveActorToSpawnData(actorData);
            }
        }

        protected override void Spawn()
        {
            Actor actor = _spawnService.Spawn(_currentActorData.Id, Teamable.TeamType, SpawnData);

            actor.Enable();

            Spawned?.Invoke(actor);

            _colorService.EstabilshColor(actor, actor.View);

            if (_actorsQueue.Count > 0)
            {
                IActorData nextData = GetNextActorData();
                EstablisCurrentActorSpawn(nextData);
            }
            else
            {
                _currentActorData = null;
            }
        }

        private void EstablisCurrentActorSpawn(IActorData actorData)
        {
            _currentActorData = actorData;
            _timer.SetWaitTime(_currentActorData.ConstructionTime);
            _timer.RestartTimer();

            SpawnStarted?.Invoke();
        }

        private IActorData GetNextActorData()
        {
            IActorData data = _actorsQueue[0];
            _actorsQueue.RemoveAt(0);

            return data;
        }

        private void RemoveActorDataFromQueue(IActorData actorData)
        {
            for (int i = 0; i < _actorsQueue.Count; i++)
            {
                if (actorData.Id == _actorsQueue[i].Id)
                {
                    _actorsQueue.RemoveAt(i);

                    return;
                }
            }
        }
    }
}