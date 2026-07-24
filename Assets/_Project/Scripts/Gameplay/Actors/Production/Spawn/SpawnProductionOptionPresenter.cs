namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public class SpawnProductionOptionPresenter : ISpawnProductionOptionPresenter
    {
        private readonly ISpawnProductionOption _model;

        public SpawnProductionOptionPresenter(ISpawnProductionOption model)
        {
            _model = model ?? throw new System.ArgumentNullException(nameof(model));
        }

        public void HandleSelectButton() =>
            _model.Execute();

        public void HandleDecrementButton() => 
            _model.CancelSpawn();
    }
}