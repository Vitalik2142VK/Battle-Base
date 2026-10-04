using BattleBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesController : IBuildingSitesController
    {
        private readonly Dictionary<int, List<RegisteredBuildingSite>> _sitesByLine;
        private readonly List<RegisteredBuildingSite> _sites;
        private readonly Randomizer _randomizer;

        public event Action<IRegisteredBuildingSite> SitesBuildCompleted;

        public BuildingSitesController(Randomizer randomizer)
        {
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _sitesByLine = new Dictionary<int, List<RegisteredBuildingSite>>();
            _sites = new List<RegisteredBuildingSite>();
        }

        public IEnumerable<IRegisteredBuildingSite> RegisteredBuildingSites => _sites;

        public int NumberSites => _sites.Count;

        public bool HasFreeSites { get; private set; }

        public IRegisteredBuildingSite Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            RegisteredBuildingSite registeredBuildingSite = new(buildingSiteActor, buildingSite);
            int numerLine = registeredBuildingSite.NumberLine;
            _sites.Add(registeredBuildingSite);

            if (_sitesByLine.ContainsKey(numerLine) == false)
                _sitesByLine.Add(numerLine, new List<RegisteredBuildingSite>());

            _sitesByLine[numerLine].Add(registeredBuildingSite);
            HasFreeSites = true;

            registeredBuildingSite.ActorAdded += OnFinishBuild;
            registeredBuildingSite.StateChanged += OnCheckFreeSide;

            return registeredBuildingSite;
        }

        public void Disable()
        {
            foreach (var site in _sites)
            {
                site.ActorAdded -= OnFinishBuild;
                site.StateChanged -= OnCheckFreeSide;
                site.Disable();
            }
        }

        public IRegisteredBuildingSite[] GetFreeSitesInLine(int lineNumber)
        {
            if (_sitesByLine.ContainsKey(lineNumber) == false)
                throw new InvalidOperationException($"{nameof(_sitesByLine)} don't constrain line {lineNumber}");

            return _sitesByLine[lineNumber]
                .Where(s => s.HasBuilding == false && s.IsConstruction == false)
                .ToArray();
        }

        public bool TryGetRandomFreeSiteInLine(int lineNumber, out IRegisteredBuildingSite buildingSite)
        {
            buildingSite = null;

            var freeSites = GetFreeSitesInLine(lineNumber);

            if (freeSites.Length == 0)
                return false;

            int randomIndex = _randomizer.GetRangeZero(freeSites.Length);
            buildingSite = freeSites[randomIndex];

            return true;
        }

        private void OnFinishBuild(RegisteredBuildingSite buildingSite) =>
            SitesBuildCompleted?.Invoke(buildingSite);

        private void OnCheckFreeSide()
        {
            foreach (var site in _sites)
            {
                if (site.HasBuilding == false)
                {
                    HasFreeSites = true;

                    return;
                }
            }

            HasFreeSites = false;
        }
    }
}