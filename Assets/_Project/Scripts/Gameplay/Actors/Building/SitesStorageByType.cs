using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class SitesStorageByType
    {
        private readonly Dictionary<SiteType, BuildingSitesController> _controllers;
        private readonly Randomizer _randomizer;

        public SitesStorageByType(Randomizer randomizer)
        {
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _controllers = new Dictionary<SiteType, BuildingSitesController>();
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
                _controllers.Add(type, new BuildingSitesController(_randomizer));

            IRegisteredBuildingSite site = _controllers[type].Register(buildingSiteActor, buildingSite);
        }

        public IBuildingSitesController GetBuildingSitesController(SiteType type)
        {
            if (_controllers.ContainsKey(type) == false)
                throw new InvalidOperationException($"{nameof(_controllers)} don't constrain key {type}");

            return _controllers[type];
        }

        public bool TryGetSiteById(int id, out IRegisteredBuildingSite buildingSite)
        {
            foreach (var controller in _controllers.Values)
            {
                if (controller.TryGetSiteById(id, out buildingSite))
                    return true;
            }

            buildingSite = null;

            return false;
        }
    }
}