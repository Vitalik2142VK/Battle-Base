using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionPresenter : IProductionPresenter
    {
        private readonly IProductionService _model;

        public ProductionPresenter(IProductionService model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public IEnumerable<IProductionOption> ProductionOptions => _model.GetProductionOptions();
    }
}