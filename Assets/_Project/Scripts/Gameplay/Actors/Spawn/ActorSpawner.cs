using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public abstract class ActorSpawner : IActorSpawner
    {
        private readonly List<ProductionOption> _productionOptions;
        private readonly List<IActorData> _actorsDatas;

        public abstract event Action<IActor> Spawned;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _actorsDatas = new List<IActorData>(actorsToCreate);
            _productionOptions = new List<ProductionOption>(_actorsDatas.Count);

            foreach (var data in _actorsDatas)
            {
                SpawnCommand command = new(this, data);
                ProductionOption productionOption = new(command, data);
                _productionOptions.Add(productionOption);
            }
        }

        public IEnumerable<ProductionOption> ProductionOptions => _productionOptions;

        protected ITeamable Teamable { get; private set; }

        protected ISpawnPoint SpawnData { get; private set; }

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
            _actorsDatas.Contains(actorData);
    }
}