using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesStorage : IBuildingSitesStorage, IDisposable
    {
        private readonly Dictionary<TeamType, SitesStorageByType> _storages;
        private readonly Random _random;

        public BuildingSitesStorage()
        {
            _storages = new Dictionary<TeamType, SitesStorageByType>();
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

            TeamType team = buildingSiteActor.TeamType;

            if (_storages.ContainsKey(team) == false)
                _storages.Add(team, new SitesStorageByType(_random));

            _storages[team].Register(buildingSiteActor, buildingSite);
        }

        public IBuildingSitesController GetBuildingSitesController(TeamType team, SiteType siteType = SiteType.Default)
        {
            if (_storages.ContainsKey(team) == false)
                throw new InvalidOperationException($"{nameof(_storages)} don't constrain key {team}");

            return _storages[team].GetBuildingSitesController(siteType);
        }

        public IRegisteredBuildingSite GetSiteById(TeamType team, int id)
        {
            if (_storages.ContainsKey(team) == false)
                throw new InvalidOperationException($"{nameof(_storages)} don't constrain key {team}");

            if (_storages[team].TryGetSiteById(id, out IRegisteredBuildingSite site) == false)
                throw new InvalidOperationException($"{team} don't constrain site with id = '{id}'");

            return site;
        }

        public IRegisteredBuildingSite GetSiteById(int id)
        {
            foreach (var storage in _storages.Values)
            {
                if (storage.TryGetSiteById(id, out IRegisteredBuildingSite site))
                    return site;
            }

            throw new InvalidOperationException($"There is no {nameof(IRegisteredBuildingSite)} with id = '{id}'");
        }
    }
}