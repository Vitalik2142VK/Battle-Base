using System;
using System.Collections.Generic;
using BattleBase.Gameplay;
using BattleBase.Gameplay.MiniMap;
using BattleBase.UI;
using UnityEngine;
using VContainer;

namespace BattleBase.EntryPoints
{
    public class GameEntryPoint : EntryPointBase
    {
        //todo change or remove
        //[SerializeField] private List<Trackable> _buildingSiteItemInfos;
        //[SerializeField] private List<ProductionItemConfig> _barracksItemInfos;
        //[SerializeField] private List<ProductionItemConfig> _machineFactoryItemInfos;
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

            //_entityFactory.SetBarracksInfos(_barracksItemInfos);
            //_entityFactory.SetMachineFactoryInfos(_machineFactoryItemInfos);
        }
    }
}