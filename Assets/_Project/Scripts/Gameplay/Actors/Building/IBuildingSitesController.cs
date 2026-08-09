using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IBuildingSitesController
    {
        public event Action<IRegisteredBuildingSite> SitesBuildCompleted;

        public IEnumerable<IRegisteredBuildingSite> RegisteredBuildingSites { get; }

        public int NumberSites { get; }

        public bool HasFreeSites { get; }

        public IRegisteredBuildingSite[] GetFreeSitesInLine(int lineNumber);

        public bool TryGetRandomFreeSiteInLine(int lineNumber, out IRegisteredBuildingSite buildingSite);
    }
}