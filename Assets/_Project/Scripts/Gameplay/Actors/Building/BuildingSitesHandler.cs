using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesHandler : IBuildingSitesHandler, IDisposable
    {
        private readonly List<RegisteredBuildingSites> _sites;

        public BuildingSitesHandler()
        {
            _sites = new List<RegisteredBuildingSites>();
        }

        public void Dispose()
        {
            foreach (var site in _sites)
                site.Disabele();
        }

        public void Register(IBuildingSite buildingSite, IActorSpawnerEvents events) => 
            _sites.Add(new RegisteredBuildingSites(buildingSite, events));
    }
}