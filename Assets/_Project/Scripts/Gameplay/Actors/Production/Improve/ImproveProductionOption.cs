using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.ImproveSystem;
using System;

namespace BattleBase.Gameplay.Actors.Production.Improve
{
    public class ImproveProductionOption : IImproveProductionOption
    {
        private readonly IProductionOption _productionOption;

        public ImproveProductionOption(
            IProductionOption productionOption, 
            IMaterialData materialData,
            IImproverState improverState)
        {
            _productionOption = productionOption ?? throw new ArgumentNullException(nameof(productionOption));

            MaterialData = materialData ?? throw new ArgumentNullException(nameof(materialData));
            ImproverState = improverState ?? throw new ArgumentNullException(nameof(improverState));
        }

        public TypeProduction Type => TypeProduction.Improve;

        public IProductionData Data => _productionOption.Data;

        public IMaterialData MaterialData { get; }

        public IImproverState ImproverState { get; }

        public void Execute() => 
            _productionOption.Execute();
    }
}