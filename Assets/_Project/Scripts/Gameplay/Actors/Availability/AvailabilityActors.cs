using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class AvailabilityActors : IAvailabilityActors
    {
        private readonly HashSet<string> _availabilityActorIds;

        public AvailabilityActors(IEnumerable<IActorConfig> configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            _availabilityActorIds = new HashSet<string>();

            Team = team;

            foreach (var config in configs)
            {
                IActorData data = config.Data;

                if (data.IsAvailable)
                    _availabilityActorIds.Add(data.Id);
            }
        }

        public TeamType Team { get; }

        public void EstablishActors(IActorSpawner spawner)
        {
            if (spawner == null)
                throw new ArgumentNullException(nameof(spawner));

#if UNITY_EDITOR // todo remove
            if (DebugSetting.IsOpenAllUnit)
            {
                spawner.DataController.EstablishActors(spawner.DataController.AvailabilityActorDatas);

                return;
            }
#endif
            ISpawnerDataController dataController = spawner.DataController;
            List<IActorData> actorDatas = new(dataController.AvailabilityActorDatas);

            for (int i = 0; i < actorDatas.Count; i++)
            {
                if (_availabilityActorIds.Contains(actorDatas[i].Id))
                    continue;

                actorDatas.RemoveAt(i--);
            }

            dataController.EstablishActors(actorDatas);
        }
    }
}