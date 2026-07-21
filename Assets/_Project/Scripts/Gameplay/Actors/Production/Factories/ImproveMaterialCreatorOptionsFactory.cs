using BattleBase.Core;
using BattleBase.Gameplay.Actors.ImproveSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class ImproveMaterialCreatorOptionsFactory : IProductionOptionsFactory
    {
        private readonly IMaterialCreatorImprover _improvement;

        public ImproveMaterialCreatorOptionsFactory(IMaterialCreatorImprover improvement)
        {
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IEnumerable<IProductionOption> Create()
        {
            List<IProductionOption> productionOptions = new();

            if (_improvement.CanImprove)
            {
                DelegateCommand command = new(() => _improvement.TryImprove());
                IProductionOption improveProductionOption = new ProductionOption
                    (command, 
                    _improvement.Data, 
                    TypeProduction.Improve);
                productionOptions.Add(improveProductionOption);
            }

            return productionOptions;
        }
    }
}