using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesStorage : IBuildingSitesStorage, IDisposable
    {
        private readonly Dictionary<SiteType, SitesStorageByType> _storages;
        private readonly Random _random;

        public BuildingSitesStorage()
        {
            _storages = new Dictionary<SiteType, SitesStorageByType>();
            _random = new Random();
        }

        public void Dispose()
        {
            foreach (var storage in _storages.Values)
                storage.Disable();
        }

        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            SiteType siteType = buildingSite.Type;

            if (_storages.ContainsKey(siteType) == false)
                _storages.Add(siteType, new SitesStorageByType(_random));

            _storages[siteType].Register(buildingSiteActor, buildingSite);
        }

        public IBuildingSitesController GetBuildingSitesController(TeamType team, SiteType siteType = SiteType.Default)
        {
            if (_storages.ContainsKey(siteType) == false)
                throw new InvalidOperationException($"{nameof(_storages)} don't constrain key {siteType}");

            return _storages[siteType].GetBuildingSitesController(team);
        }
    }
}