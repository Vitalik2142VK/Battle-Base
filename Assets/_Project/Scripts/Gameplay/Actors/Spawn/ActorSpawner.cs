using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawner : IActorSpawner
    {
        private readonly List<IActorData> _actorsToCreate;
        private readonly IActorSpawnService _spawnService;

        private ITeamable _teamable;
        private IActorData _currentActorData;
        private bool _isDisable;

        public event Action<Actor> Spawned;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate, IActorSpawnService actorSpawnService)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsToCreate = new List<IActorData>(actorsToCreate);
            _spawnService = actorSpawnService ?? throw new ArgumentNullException(nameof(actorSpawnService));
            Timer = new();
        }

        public IEnumerable<IActorData> ActorsData => _actorsToCreate.ToArray();

        protected Timer Timer { get; }

        public void Init(ITeamable teamable)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
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

            if (Timer.IsTimeUp == false)
            {
                Timer.Tick(delta);

                return;
            }

            if (_spawnService.TrySpawn(_currentActorData.Prefab.name, out Actor actor))
            {
                Spawned?.Invoke(actor);

                actor.Enable();
                actor.SetTeam(_teamable.TeamType);

                ActionOnSpawned();
            }
        }

        public void SelectActorData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_actorsToCreate.Contains(actorData))
            {
                _currentActorData = actorData;
                Timer.SetWaitTime(_currentActorData.ConstructionTime);
                Timer.RestartTimer();
            }
            else
            {
                throw new InvalidOperationException($"{nameof(actorData)} not found");
            }
        }

        protected virtual void ActionOnSpawned()
        {
            _currentActorData = null;
        }
    }
}