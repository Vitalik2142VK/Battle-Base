using BattleBase.Gameplay.Actors.Spawn;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSitesController
    {
        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite);

        public IEnumerable<IRegisteredBuildingSite> GetRegisteredBuildingSites(TeamType team);
    }
}