using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesController : IBuildingSitesController, IDisposable
    {
        private readonly Dictionary<TeamType, List<RegisteredBuildingSite>> _sites;

        public BuildingSitesController()
        {
            _sites = new Dictionary<TeamType, List<RegisteredBuildingSite>>();
        }

        public void Dispose()
        {
            foreach (var siteList in _sites.Values)
            {
                foreach (var site in siteList)
                    site.Disable();
            }
        }

        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            TeamType team = buildingSiteActor.TeamType;

            if (_sites.ContainsKey(team) == false)
                _sites.Add(team, new List<RegisteredBuildingSite>());

            _sites[team].Add(new RegisteredBuildingSite(buildingSiteActor, buildingSite));
        }

        public IEnumerable<IRegisteredBuildingSite> GetRegisteredBuildingSites(TeamType team)
        {
            if (_sites.ContainsKey(team) == false)
                throw new InvalidOperationException($"{nameof(_sites)} don't constrain key {team}");

            return _sites[team];
        }

        public IRegisteredBuildingSite[] GetFreeRegisteredBuildingSites(TeamType team, int lineNumber)
        {
            if (lineNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(lineNumber));

            return _sites[team]
                .Where(r => r.NumberLine == lineNumber && r.HasBuilding == false)
                .ToArray();
        }
    }
}