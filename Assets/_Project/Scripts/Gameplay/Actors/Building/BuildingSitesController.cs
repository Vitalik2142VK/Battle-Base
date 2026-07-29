using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesController : IBuildingSitesController
    {
        private readonly Dictionary<int, List<RegisteredBuildingSite>> _sitesByLine;
        private readonly List<RegisteredBuildingSite> _sites;
        private readonly Random _random;

        public event Action<IRegisteredBuildingSite> SiteChanged;

        public BuildingSitesController()
        {
            _sitesByLine = new Dictionary<int, List<RegisteredBuildingSite>>();
            _sites = new List<RegisteredBuildingSite>();
            _random = new Random();
        }

        public IEnumerable<IRegisteredBuildingSite> RegisteredBuildingSites => _sites;

        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            RegisteredBuildingSite registeredBuildingSite = new(buildingSiteActor, buildingSite);
            int numerLine = registeredBuildingSite.NumberLine;

            registeredBuildingSite.ActorAdded += OnChangeSite;
            _sites.Add(registeredBuildingSite);


            if (_sitesByLine.ContainsKey(numerLine) == false)
                _sitesByLine.Add(numerLine, new List<RegisteredBuildingSite>());

            _sitesByLine[numerLine].Add(registeredBuildingSite);
        }

        public void Disable()
        {
            foreach (var site in _sites)
            {
                site.ActorAdded -= OnChangeSite;
                site.Disable();
            }
        }

        public IRegisteredBuildingSite[] GetFreeSitesInLine(int lineNumber)
        {
            if (_sitesByLine.ContainsKey(lineNumber) == false)
                throw new InvalidOperationException($"{nameof(_sitesByLine)} don't constrain line {lineNumber}");

            return _sitesByLine[lineNumber]
                .Where(r => r.HasBuilding == false && r.IsConstruction == false)
                .ToArray();
        }

        public bool TryGetRandomFreeSiteInLine(int lineNumber, out IRegisteredBuildingSite buildingSite)
        {
            buildingSite = null;

            var freeSites = GetFreeSitesInLine(lineNumber);

            if (freeSites.Length == 0)
                return false;

            int randomIndex = _random.Next(freeSites.Length);
            buildingSite = freeSites[randomIndex];

            return true;
        }

        private void OnChangeSite(RegisteredBuildingSite buildingSite) =>
            SiteChanged?.Invoke(buildingSite);
    }
}