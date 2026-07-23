using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public abstract class ActorSpawner : IActorSpawner
    {
        private readonly Dictionary<string, SpawnProductionData> _spawnDatas;
        private readonly IMaterialRegistry _materialRegistry;

        private MatetialTransaction _currentTransaction;
        private SpawnProductionData _currnetSpawnData;

        public abstract event Action<IActor> Spawned;

        public ActorSpawner(IEnumerable<IActorData> actorsToCreate, IMaterialRegistry materialRegistry)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _spawnDatas = new Dictionary<string, SpawnProductionData>();

            foreach (var actor in actorsToCreate)
                _spawnDatas.Add(actor.Id, new SpawnProductionData(actor));
        }

        public IEnumerable<ISpawnProductionData> SpawnDatas => _spawnDatas.Values;

        protected bool IsInProcessSpawn => _currnetSpawnData != null;

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

        public abstract void CancelSpawnActor(IActorData actorData);

        protected abstract void Spawn();

        protected bool ConstrainActorData(IActorData actorData) =>
            _spawnDatas.ContainsKey(actorData.Id);

        protected void AddActorToSpawnData(IActorData actorData)
        {
            if (_spawnDatas.TryGetValue(actorData.Id, out SpawnProductionData data) == false)
                throw new InvalidOperationException($"{nameof(IActor)} with id - {nameof(actorData.Id)} not found");

            data.IncreaseCount();
            data.UpdateData();
        }

        protected void RemoveActorToSpawnData(IActorData actorData)
        {
            if (_spawnDatas.TryGetValue(actorData.Id, out SpawnProductionData data) == false)
                throw new InvalidOperationException($"{nameof(IActor)} with id - {nameof(actorData.Id)} not found");

            data.ReduceCount();
            data.ResetTimeSpent();
            data.UpdateData();
        }

        protected void CalcualteProcessSpawn(float delta) =>
            _currnetSpawnData.CalculateProcess(delta);

        protected void CancelSpawn()
        {
            _currentTransaction.Cancle();

            Reset();
        }

        protected void FinishSpawn()
        {
            _currentTransaction.Finish();

            Reset();
        }

        protected bool CanBeginSpawn(IActorData actorData)
        {
            if (_spawnDatas.TryGetValue(actorData.Id, out SpawnProductionData data) == false)
                throw new InvalidOperationException($"{nameof(IActor)} with id - {nameof(actorData.Id)} not found");

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