using BattleBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesController : IBuildingSitesController
    {
        private readonly Dictionary<int, List<RegisteredBuildingSite>> _sitesByLines;
        private readonly List<RegisteredBuildingSite> _sites;
        private readonly Randomizer _randomizer;

        public event Action<IRegisteredBuildingSite> SitesBuildCompleted;

        public BuildingSitesController(Randomizer randomizer)
        {
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _sitesByLines = new Dictionary<int, List<RegisteredBuildingSite>>();
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

            if (_sitesByLines.ContainsKey(numerLine) == false)
                _sitesByLines.Add(numerLine, new List<RegisteredBuildingSite>());

            _sitesByLines[numerLine].Add(registeredBuildingSite);
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

        public bool TryGetFreeSitesInLine(int lineNumber, out IRegisteredBuildingSite[] freeSites)
        {
            if (_sitesByLines.ContainsKey(lineNumber) == false)
                throw new InvalidOperationException($"{nameof(_sitesByLines)} don't constrain line {lineNumber}");

            freeSites = null;

            if (HasFreeSitesInLine(lineNumber) == false)
                return false;

            freeSites = _sitesByLines[lineNumber]
                .Where(s => s.HasBuilding == false && s.IsConstruction == false)
                .ToArray();

            return true;
        }

        public bool TryGetRandomFreeSite(out IRegisteredBuildingSite buildingSite)
        {
            buildingSite = null;

            if (HasFreeSites == false)
                return false;

            foreach (var numberLine in _sitesByLines.Keys)
            {
                if (TryGetRandomFreeSiteInLine(numberLine, out buildingSite))
                    return true;
            }

            return false;
        }

        public bool TryGetRandomFreeSiteInLine(int lineNumber, out IRegisteredBuildingSite buildingSite)
        {
            buildingSite = null;

            if (TryGetFreeSitesInLine(lineNumber, out IRegisteredBuildingSite[] freeSites) == false)
                return false;

            int randomIndex = _randomizer.GetRangeZero(freeSites.Length);
            buildingSite = freeSites[randomIndex];

            return true;
        }

        private bool HasFreeSitesInLine(int lineNumber)
        {
            if (_sitesByLines.ContainsKey(lineNumber) == false)
                throw new InvalidOperationException($"{nameof(_sitesByLines)} don't constrain line {lineNumber}");

            foreach (var site in _sitesByLines[lineNumber])
            {
                if (site.HasBuilding == false && site.IsConstruction == false)
                    return true;
            }

            return false;
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