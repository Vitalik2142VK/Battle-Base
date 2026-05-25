using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class MultiActorSpawner : IActorSpawner
    {
        private readonly List<IActorData> _actorsToCreate;
        private readonly Queue<IActorData> _actorsQueue;
        private readonly IActorSpawnService _spawnService;
        private readonly Timer _timer;

        private ITeamable _teamable;
        private ISpawnData _spawnData;
        private IActorData _currentActorData;
        private bool _isDisable;

        public event Action<Actor> Spawned;

        public MultiActorSpawner(IEnumerable<IActorData> actorsToCreate, IActorSpawnService actorSpawnService)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsToCreate = new List<IActorData>(actorsToCreate);
            _actorsQueue = new Queue<IActorData>();

            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            _timer = new();
        }

        public IEnumerable<IActorData> ActorsData => _actorsToCreate.ToArray();


        public void Init(ITeamable teamable, ISpawnData spawnData)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
            _spawnData = spawnData ?? throw new ArgumentNullException(nameof(spawnData));
        }

        public virtual void Enable()
        {
            _isDisable = false;
        }

        public virtual void Disable()
        {
            _isDisable = true;
            _currentActorData = null;
        }

        public void Update(float delta)
        {
            if (_currentActorData == null || _isDisable)
                return;

            _timer.Tick(delta);

            if (_timer.IsTimeUp)
                ProcessSpawn();
        }

        public void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_actorsToCreate.Contains(actorData))
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
            if (_spawnService.TrySpawn(_currentActorData.Prefab.name, _spawnData, out Actor actor))
            {
                Spawned?.Invoke(actor);

                actor.Enable();
                actor.SetTeam(_teamable.TeamType);

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