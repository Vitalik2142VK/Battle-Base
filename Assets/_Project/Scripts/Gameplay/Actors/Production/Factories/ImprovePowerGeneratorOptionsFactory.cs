using BattleBase.Core;
using BattleBase.Gameplay.Actors.ImproveSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class ImprovePowerGeneratorOptionsFactory : IProductionOptionsFactory
    {
        private readonly IPowerGeneratorImprover _improvement;

        public ImprovePowerGeneratorOptionsFactory(IPowerGeneratorImprover improvement)
        {
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IEnumerable<ProductionOption> Create()
        {
            List<ProductionOption> productionOptions = new();

            if (_improvement.CanImprove)
            {
                DelegateCommand command = new(() => _improvement.Improve());
                ProductionOption improveProductionOption = new(command, _improvement.Data, TypeProduction.Improve);
                productionOptions.Add(improveProductionOption);
            }

            return productionOptions;
        }
    }
}