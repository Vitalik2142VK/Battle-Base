namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSitesStorage
    {
        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite);

        public IBuildingSitesController GetBuildingSitesController(
            TeamType team, 
            SiteType siteType = SiteType.Default);

        public IRegisteredBuildingSite GetSiteById(TeamType team, int id);

        public IRegisteredBuildingSite GetSiteById(int id);
    }
}