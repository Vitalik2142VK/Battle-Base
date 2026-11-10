namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IActorUpgrader
    {
        public void UpgradeActorComponents(TeamType teamType, IActor actor);
    }
}