using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public abstract class ActorSpawner : IActorSpawner
    {
        private readonly SpawnerDataController _dataController;
        private readonly IMaterialRegistry _materialRegistry;

        private MatetialTransaction _currentTransaction;
        private SpawnProductionData _currnetSpawnData;

        public abstract event Action<IActor> Spawned;
        public abstract event Action SpawnStarted;
        public abstract event Action SpawnCancled;
        public abstract event Action SpawnFinished;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate, IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));

            _dataController = new SpawnerDataController(actorsToCreate);
        }

        public Type KeyType => typeof(IActorSpawner);

        public IEnumerable<ISpawnProductionData> SpawnDatas => _dataController.SpawnProductionDatas;

        public ISpawnerDataController DataController => _dataController;

        public bool IsInProcessSpawn => _currnetSpawnData != null;

        protected ITeamable Teamable { get; private set; }

        protected ISpawnPoint SpawnData { get; private set; }


        public void Init(ITeamable teamable, ISpawnPoint spawnData)
        {
            Teamable ??= teamable ?? throw new ArgumentNullException(nameof(teamable));
            SpawnData ??= spawnData ?? throw new ArgumentNullException(nameof(spawnData));
        }

        public abstract void Update(float delta);

        public abstract void SelectActorData(IActorData actorData);

        public abstract void CancelSpawnActor(IActorData actorData);

        protected abstract void Spawn();

        public virtual void Enable() => 
            _currentTransaction = null;

        public virtual void Disable() => 
            _dataController.Disable();

        protected void AddActorToSpawnData(IActorData actorData)
        {
            SpawnProductionData data = _dataController.GetSpawnProductionData(actorData);
            data.IncreaseCount();
            data.UpdateData();
        }

        protected void RemoveActorToSpawnData(IActorData actorData)
        {
            SpawnProductionData data = _dataController.GetSpawnProductionData(actorData);
            data.ReduceCount();
            data.ResetTimeSpent();
            data.UpdateData();
        }

        protected void CalcualteProcessSpawn(float delta) =>
            _currnetSpawnData.CalculateProcess(delta);

        protected void CancelSpawn()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Cancle();

                Reset();
            }
        }

        protected void FinishSpawn()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Finish();

                Reset();
            }
        }

        protected bool CanBeginSpawn(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            SpawnProductionData data = _dataController.GetSpawnProductionData(actorData);

            if (_materialRegistry.TryGetTransaction(Teamable.TeamType, actorData.Price, out _currentTransaction))
            {
                _currentTransaction.Init(() => Spawn());
                _currnetSpawnData = data;
                _currnetSpawnData.ResetTimeSpent();
                _currnetSpawnData.ReduceCount();
                _currnetSpawnData.UpdateData();

                return true;
            }

            _currnetSpawnData = null;

            return false;
        }

        private void Reset()
        {
            _currnetSpawnData.ResetTimeSpent();
            _currnetSpawnData.UpdateData();
            _currentTransaction = null;
            _currnetSpawnData = null;
        }
    }
}