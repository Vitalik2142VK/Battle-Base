namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IActorComponentUpgrader
    {
        public TeamType Team { get; }

        public void UpgradeActorComponents(IActor actor);
    }
}