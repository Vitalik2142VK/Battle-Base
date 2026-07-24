using BattleBase.Core;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production.Improve;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class ImproveMaterialCreatorOptionsFactory : IProductionOptionsFactory
    {
        private readonly IMaterialCreatorImprover _improver;

        public ImproveMaterialCreatorOptionsFactory(IMaterialCreatorImprover improver)
        {
            _improver = improver ?? throw new ArgumentNullException(nameof(improver));
        }

        public IEnumerable<IProductionOption> Create()
        {
            List<IProductionOption> productionOptions = new();

            if (_improver.CanImprove)
            {
                DelegateCommand command = new(() => _improver.TryImprove());
                ProductionOption productionOption = new(command, _improver.Data, TypeProduction.Improve);
                IImproveProductionData data = _improver.Data;
                ImproveProductionOption improveProductionOption = new(
                    productionOption,
                    data.MaterialData,
                    data);
                productionOptions.Add(improveProductionOption);
            }

            return productionOptions;
        }
    }
}