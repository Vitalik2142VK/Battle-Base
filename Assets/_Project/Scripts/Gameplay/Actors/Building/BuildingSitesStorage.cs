using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSitesStorage : IBuildingSitesStorage, IDisposable
    {
        private readonly Dictionary<TeamType, BuildingSitesController> _controllers;

        public BuildingSitesStorage()
        {
            _controllers = new Dictionary<TeamType, BuildingSitesController>();
        }

        public void Dispose()
        {
            foreach (var controller in _controllers.Values)
                controller.Disable();
        }

        public void Register(IActor buildingSiteActor, IBuildingSite buildingSite)
        {
            TeamType team = buildingSiteActor.TeamType;

            if (_controllers.ContainsKey(team) == false)
                _controllers.Add(team, new BuildingSitesController());

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