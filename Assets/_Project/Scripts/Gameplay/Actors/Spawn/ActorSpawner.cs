using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawner : IActorSpawner
    {
        private readonly List<IActorData> _actorsToCreate;
        private readonly IActorSpawnService _spawnService;
        private readonly IActorColorService _colorService;
        private readonly Timer _timer;

        private ITeamable _teamable;
        private ISpawnData _spawnData;
        private IActorData _currentActorData;
        private bool _isDisable;

        public event Action<Actor> Spawned;

        public ActorSpawner(
            IEnumerable<IActorData> actorsToCreate, 
            IActorSpawnService actorSpawnService,
            IActorColorService colorService)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsToCreate = new List<IActorData>(actorsToCreate);
            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            _colorService = colorService ?? throw new ArgumentNullException(nameof(colorService));
            _timer = new();
        }

        public IEnumerable<IActorData> ActorsData => _actorsToCreate.ToArray();

        public void Init(ITeamable teamable, ISpawnData spawnData)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
            _spawnData = spawnData ?? throw new ArgumentNullException(nameof(spawnData));
        }

        public void Enable()
        {
            _isDisable = false;
        }

        public void Disable()
        {
            _isDisable = true;
            _currentActorData = null;
        }

        public void Update(float delta)
        {
            if (_currentActorData == null || _isDisable)
                return;

            if (_timer.IsTimeUp == false)
            {
                _timer.Tick(delta);

                return;
            }

            if (_spawnService.TrySpawn(_currentActorData.Prefab.name, _spawnData, out Actor actor))
            {
                Spawned?.Invoke(actor);

                TeamType team = _teamable.TeamType;
                actor.Enable();
                actor.SetTeam(team);

                _colorService.EstabilshColor(actor, actor.View);
                _currentActorData = null;
            }
        }

        public void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_actorsToCreate.Contains(actorData))
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
    }
}