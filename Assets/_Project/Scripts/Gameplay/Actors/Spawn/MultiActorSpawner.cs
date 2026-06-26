using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class MultiActorSpawner : ActorSpawner
    {
        private readonly Queue<IActorData> _actorsQueue;
        private readonly IActorSpawnService _spawnService;
        private readonly IActorColorService _colorService;
        private readonly Timer _timer;

        private IActorData _currentActorData;
        private bool _isDisable;

        public override event Action<IActor> Spawned;

        public MultiActorSpawner(
            IEnumerable<IActorData> actorsToCreate, 
            IActorSpawnService actorSpawnService,
            IActorColorService colorService) : base (actorsToCreate)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsQueue = new Queue<IActorData>();

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

            _timer.Tick(delta);

            if (_timer.IsTimeUp)
                ProcessSpawn();
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
                    _actorsQueue.Enqueue(actorData);
            }
            else
            {
                throw new InvalidOperationException($"{nameof(actorData)} not found");
            }
        }

        private void ProcessSpawn()
        {
            if (_spawnService.TrySpawn(_currentActorData.Prefab.name, SpawnData, out Actor actor))
            {
                Spawned?.Invoke(actor);

                TeamType team = Teamable.TeamType;
                actor.Enable();
                actor.SetTeam(team);

                _colorService.EstabilshColor(actor, actor.View);

                if (_actorsQueue.Count > 0)
                    EstablisCurrentActorSpawn(_actorsQueue.Dequeue());
                else
                    _currentActorData = null;
            }
        }

        private void EstablisCurrentActorSpawn(IActorData actorData)
        {
            _currentActorData = actorData;
            _timer.SetWaitTime(_currentActorData.ConstructionTime);
            _timer.RestartTimer();
        }
    }
}