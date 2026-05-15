using System;
using BattleBase.Gameplay;
using BattleBase.Gameplay.MiniMap;
using UnityEngine;
using VContainer;

namespace BattleBase.EntryPoints
{
    public class GameEntryPoint : EntryPointBase
    {
        [SerializeField] private Transform _environment;

        private IEntityTrackerFactory _trackerFactory;

        [Inject]
        public void Construct(IEntityTrackerFactory trackerFactory) =>
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));

        protected override void Start()
        {
            base.Start();

            foreach (Entity entity in _environment.GetComponentsInChildren<Entity>(false))
            {
                if (entity is RoadLane or Stronghold or IBuildingSite)
                    _trackerFactory.CreateTracker(entity, PositionTrackingType.Static);
            }
        }
    }
}