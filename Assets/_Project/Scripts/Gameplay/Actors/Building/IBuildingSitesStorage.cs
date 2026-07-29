namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSitesStorage
    {
        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite);

        public IBuildingSitesController GetBuildingSitesController(TeamType team);
    }
}