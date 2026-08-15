namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionPresenter : IProductionPresenter
    {
        private readonly IProductionService _model;

        public ProductionPresenter(IProductionService model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public int BuildingSiteId => _model.BuildingSiteId;
    }
}