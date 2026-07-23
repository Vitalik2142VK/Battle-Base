using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem.Removal;
using BattleBase.Gameplay.Actors.Production.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class DemolitionOptionsFactory : IProductionOptionsFactory
    {
        private readonly IDemolition _demolition;

        public DemolitionOptionsFactory(IDemolition demolition)
        {
            _demolition = demolition ?? throw new ArgumentNullException(nameof(demolition));
        }

        public IEnumerable<IProductionOption> Create()
        {
            DelegateCommand command = new(() => _demolition.Demolish());

            return new IProductionOption[]
            { 
                //new SpawnProductionOption(command, _demolition.Data, TypeProduction.Removal) 
            };
        }
    }
}