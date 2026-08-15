using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class SitesStorageByType
    {
        private readonly Dictionary<SiteType, BuildingSitesController> _controllers;
        private readonly Dictionary<int, IRegisteredBuildingSite> _registeredSites;
        private readonly Random _random;

        public SitesStorageByType(Random random = null)
        {
            _random = random ?? new Random();

            _controllers = new Dictionary<SiteType, BuildingSitesController>();
            _registeredSites = new Dictionary<int, IRegisteredBuildingSite>();
        }

        public void Disable()
        {
            foreach (var controller in _controllers.Values)
                controller.Disable();
        }

        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            SiteType type = buildingSite.Type;

            if (_controllers.ContainsKey(type) == false)
                _controllers.Add(type, new BuildingSitesController(_random));

            IRegisteredBuildingSite site = _controllers[type].Register(buildingSiteActor, buildingSite);

            if (_registeredSites.ContainsKey(site.BuildingSiteId))
                throw new InvalidOperationException($"{nameof(_registeredSites)} already contains site id = {site.BuildingSiteId}");

            _registeredSites.Add(site.BuildingSiteId, site);
        }

        public IBuildingSitesController GetBuildingSitesController(SiteType type)
        {
            if (_controllers.ContainsKey(type) == false)
                throw new InvalidOperationException($"{nameof(_controllers)} don't constrain key {type}");

            return _controllers[type];
        }

        public bool TryGetSiteById(int id, out IRegisteredBuildingSite buildingSite) =>
            _registeredSites.TryGetValue(id, out buildingSite);
    }
}