using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public class EntityTrackerFactory : IEntityTrackerFactory
    {
        private readonly IEntityTrackersRegistry _entityRegistry;
        private readonly IEntitySizeCalculator _sizeCalculator;
        private readonly IUpdater _updater;

        public EntityTrackerFactory(IEntityTrackersRegistry entityRegistry, IEntitySizeCalculator sizeCalculator, IUpdater updater)
        {
            _entityRegistry = entityRegistry ?? throw new ArgumentNullException(nameof(entityRegistry));
            _sizeCalculator = sizeCalculator ?? throw new ArgumentNullException(nameof(sizeCalculator));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
        }

        public IEntityTracker CreateTracker(ITrackable entity, PositionTrackingType positionTrackingType)
        {
            Transform transform = entity.Transform;
            IEntitySizeTracker sizeTracker = new StaticSizeTracker(transform, _sizeCalculator);
            IEntityPositionTracker positionTracker = CreatePositionTracker(transform, positionTrackingType);
            IEntityRotationTracker rotationTracker = new FixedRotationTracker(transform);
            IEntityTracker tracker = new EntityTracker(entity, sizeTracker, positionTracker, rotationTracker);

            _entityRegistry.Register(tracker);

            return tracker;
        }

        private IEntityPositionTracker CreatePositionTracker(Transform transform, PositionTrackingType trackingType)
        {
            return trackingType switch
            {
                PositionTrackingType.Static => new StaticPositionTracker(transform),
                PositionTrackingType.PerFrame => new PerFramePositionTracker(transform, _updater),
                _ => throw new NotImplementedException(),
            };
        }
    }
}