using BattleBase.Gameplay.Actors.Economy;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public abstract class ActorSpawner : IActorSpawner
    {
        private readonly List<IActorData> _actorDatas;
        private readonly IMaterialRegistry _materialRegistry;

        public abstract event Action<IActor> Spawned;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate, IMaterialRegistry materialRegistry)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _actorDatas = new List<IActorData>(actorsToCreate);
        }

        public IEnumerable<IActorData> ActorDatas => _actorDatas;

        protected ITeamable Teamable { get; private set; }

        protected ISpawnPoint SpawnData { get; private set; }

        protected bool IsInProcessSpawn { get; private set; }

        public void Init(ITeamable teamable, ISpawnPoint spawnData)
        {
            Teamable ??= teamable ?? throw new ArgumentNullException(nameof(teamable));
            SpawnData ??= spawnData ?? throw new ArgumentNullException(nameof(spawnData));
        }

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Update(float delta);

        public abstract void SelectActorData(IActorData actorData);

        protected bool ConstrainActorData(IActorData actorData) =>
            _actorDatas.Contains(actorData);

        protected bool CanBeginSpawn(IActorData actorData)
        {
            if (_materialRegistry.TrySpend(Teamable.TeamType, actorData.Price))
            {
                IsInProcessSpawn = true;

                return true;
            }

            return false;
        }
        
        protected void FinishSpawn()
        {
            IsInProcessSpawn = false;
        }
    }
}