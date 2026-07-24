namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionOptionPresenter : IProductionOptionPresenter
    {
        private readonly IProductionOption _model;

        public ProductionOptionPresenter(IProductionOption model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public void HandleSelectButton() =>
            _model.Execute();
    }
}