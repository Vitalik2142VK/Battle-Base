using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawner : IActorSpawner
    {
        private readonly HashSet<IActorData> _actorsToCreate;
        private readonly List<IActorData> _actorsData;
        private readonly IActorSpawnService _spawnService;
        private readonly Timer _timer;

        private ITeamable _teamable;
        private IActorData _currentActorData;

        public event Action<Actor> Spawned;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate, IActorSpawnService actorSpawnService)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsToCreate = new HashSet<IActorData>(actorsToCreate);
            _actorsData = new List<IActorData>(actorsToCreate);
            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            _currentActorData = _actorsData[0];
            _timer = new(_currentActorData.ConstructionTime);
        }

        public IEnumerable<IActorData> ActorsData => _actorsData.ToArray();

        public void Init(ITeamable teamable)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public void Enable()
        {
            _timer.SetWaitTime(_currentActorData.ConstructionTime);
        }

        public void Disable()
        {
            _currentActorData = _actorsData[0];
        }

        public void Update(float delta)
        {
            if (_timer.IsTimeUp)
            {
                _timer.Tick(delta);

                return;
            }

            if (_spawnService.TrySpawn(_currentActorData.Prefab.name, _teamable.TeamType, out Actor actor))
            {
                Spawned?.Invoke(actor);

                _timer.RestartTimer();
            }
        }

        public void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_actorsToCreate.TryGetValue(actorData, out IActorData actualValue))
            {
                _currentActorData = actualValue;
                _timer.SetWaitTime(_currentActorData.ConstructionTime);
                _timer.RestartTimer();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(actorData)} not found");
            }
        }
    }
}