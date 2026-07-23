namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IActorUpgraderRegistry
    {
        public void UpgradeActorComponents(TeamType teamType, IActor actor);
    }
}