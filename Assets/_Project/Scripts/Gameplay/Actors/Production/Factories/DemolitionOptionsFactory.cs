using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem.Removal;
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

        public IEnumerable<ProductionOption> Create()
        {
            DelegateCommand command = new(() => _demolition.Demolish());

            return new ProductionOption[]
            { 
                new(command, _demolition.Data) 
            };
        }
    }
}