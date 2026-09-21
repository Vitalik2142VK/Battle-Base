using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class PlayerAvailabilityActors : IAvailabilityActors
    {
        private readonly HashSet<string> _availabilityActorIds;

        public PlayerAvailabilityActors(IAvailableActorConfigProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _availabilityActorIds = new HashSet<string>();

            foreach (var config in provider.ActualPlayerConfigs)
            {
                IActorData data = config.Data;

                if (data.IsAvailable)
                    _availabilityActorIds.Add(data.Id);
            }
        }

        public TeamType Team => TeamType.Player;

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