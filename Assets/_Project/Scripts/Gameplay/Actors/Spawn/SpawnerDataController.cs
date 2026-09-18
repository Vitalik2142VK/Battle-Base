using BattleBase.Gameplay.Actors.Production.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class SpawnerDataController : ISpawnerDataController
    {
        private readonly Dictionary<string, SpawnProductionData> _spawnDatas;
        private readonly HashSet<string> _actorsId;

        public SpawnerDataController(IEnumerable<IActorData> actorsToCreate)
        {
            AvailabilityActorDatas = actorsToCreate ?? throw new ArgumentNullException(nameof(actorsToCreate));

            _spawnDatas = new Dictionary<string, SpawnProductionData>();
            _actorsId = new HashSet<string>();

            foreach (var actor in actorsToCreate)
            {
                _spawnDatas.Add(actor.Id, new SpawnProductionData(actor));
                _actorsId.Add(actor.Id);
            }
        }

        public IEnumerable<ISpawnProductionData> SpawnDatas => _spawnDatas.Values;

        public IEnumerable<IActorData> AvailabilityActorDatas { get; }

        public virtual void Disable()
        {
            foreach (var spawnData in _spawnDatas.Values)
                spawnData.Disable();
        }

        public bool ConstrainActorData(IActorData actorData) =>
            _spawnDatas.ContainsKey(actorData.Id);

        public void EstablishActors(IEnumerable<IActorData> actorDatas)
        {
            if (actorDatas == null)
                throw new ArgumentNullException(nameof(actorDatas));

            _spawnDatas.Clear();

            foreach (var actorData in actorDatas)
            {
                if (_actorsId.Contains(actorData.Id))
                    _spawnDatas.Add(actorData.Id, new SpawnProductionData(actorData));
            }
        }

        public SpawnProductionData GetSpawnProductionData(IActorData actorData)
        {
            if (actorData == null)
                throw new ArgumentNullException(nameof(actorData));

            if (_spawnDatas.ContainsKey(actorData.Id) == false)
                throw new InvalidOperationException($"{nameof(IActor)} with id - {nameof(actorData.Id)} not found");

            return _spawnDatas[actorData.Id];
        }
    }
}