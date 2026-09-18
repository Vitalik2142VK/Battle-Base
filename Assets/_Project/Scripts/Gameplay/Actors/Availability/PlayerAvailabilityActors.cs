using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.ShopSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class PlayerAvailabilityActors : IAvailabilityActors
    {
        private readonly HashSet<string> _availabilityActorIds;

        public PlayerAvailabilityActors(IActorsUpgradeModel actorsUpgradeModel)
        {
            if (actorsUpgradeModel == null)
                throw new ArgumentNullException(nameof(actorsUpgradeModel));

            _availabilityActorIds = new HashSet<string>();

            foreach (var info in actorsUpgradeModel.Infos)
            {
                if (info.IsAvailable)
                    _availabilityActorIds.Add(info.Id);
            }
        }

        public TeamType Team => TeamType.Player;

        public void EstablishActors(IActorSpawner spawner)
        {
            if (spawner == null)
                throw new ArgumentNullException(nameof(spawner));

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