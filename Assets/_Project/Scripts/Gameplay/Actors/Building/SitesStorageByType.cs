using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class SitesStorageByType
    {
        private readonly Dictionary<TeamType, BuildingSitesController> _controllers;
        private readonly Random _random;

        public SitesStorageByType(Random random = null)
        {
            _random = random ?? new Random();
            _controllers = new Dictionary<TeamType, BuildingSitesController>();
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

            TeamType team = buildingSiteActor.TeamType;

            if (_controllers.ContainsKey(team) == false)
                _controllers.Add(team, new BuildingSitesController(_random));

            _controllers[team].Register(buildingSiteActor, buildingSite);
        }

        public IBuildingSitesController GetBuildingSitesController(TeamType team)
        {
            if (_controllers.ContainsKey(team) == false)
                throw new InvalidOperationException($"{nameof(_controllers)} don't constrain key {team}");

            return _controllers[team];
        }
    }
}