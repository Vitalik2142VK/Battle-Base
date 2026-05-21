using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.MiniMap;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public class EntityFactory : IEntityFactory
    {
        private List<IActorData> _barracksItemInfos;
        private List<IActorData> _machineFactoryItemInfos;
        private readonly IEntityTrackerFactory _trackerFactory;

        public EntityFactory(IEntityTrackerFactory trackerFactory)
        {
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
        }

        public void SetBarracksInfos(IReadOnlyList<IActorData> infos)
        {
            if (infos == null)
                throw new ArgumentNullException(nameof(_barracksItemInfos));

            _barracksItemInfos = new(infos);
        }

        public void SetMachineFactoryInfos(IReadOnlyList<IActorData> infos)
        {
            if (infos == null)
                throw new ArgumentNullException(nameof(_barracksItemInfos));

            _machineFactoryItemInfos = new(infos);
        }

        public ITrackable Create(Trackable prefab, Transform target)
        {
            ITrackable entity = UnityEngine.Object.Instantiate(prefab, target.position, target.rotation);
            _trackerFactory.CreateTracker(entity, PositionTrackingType.Static);

            return entity;
        }
    }
}