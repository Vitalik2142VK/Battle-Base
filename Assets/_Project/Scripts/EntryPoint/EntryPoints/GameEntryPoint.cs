using System;
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
        public void Construct(IEntityTrackerFactory trackerFactory)
        {
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
        }

        protected override void Start()
        {
            base.Start();

            foreach (Trackable trackable in _environment.GetComponentsInChildren<Trackable>(false))
                _trackerFactory.CreateTracker(trackable, PositionTrackingType.Static);
        }
    }
}