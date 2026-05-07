using System;
using System.Collections.Generic;
using BattleBase.Gameplay;
using BattleBase.Gameplay.MiniMap;
using UnityEngine;
using VContainer;

namespace BattleBase.EntryPoints
{
    public class GameEntryPoint : EntryPointBase
    {
        [SerializeField] private List<Stronghold> _strongholds;

        private IMiniMapObjectRegistry _miniMapObjectRegistry;

        [Inject]
        public void Construct(IMiniMapObjectRegistry miniMapObjectRegistry) =>
            _miniMapObjectRegistry = miniMapObjectRegistry ?? throw new ArgumentNullException(nameof(miniMapObjectRegistry));


        protected override void Start()
        {
            base.Start();

            foreach (Stronghold stronghold in _strongholds)
                _miniMapObjectRegistry.Register(stronghold);
        }
    }
}