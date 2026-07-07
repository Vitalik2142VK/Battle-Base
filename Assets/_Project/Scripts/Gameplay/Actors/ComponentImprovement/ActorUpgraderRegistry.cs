using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class ActorUpgraderRegistry : IActorUpgraderRegistry
    {
        private readonly Dictionary<TeamType, List<IActorComponentUpgrader>> _upgraders;

        public ActorUpgraderRegistry(IEnumerable<IActorComponentUpgrader> upgraders)
        {
            if (upgraders == null)
                throw new ArgumentNullException(nameof(upgraders));

            _upgraders = new Dictionary<TeamType, List<IActorComponentUpgrader>>();

            foreach (var upgrader in upgraders)
            {
                if (_upgraders.ContainsKey(upgrader.Team) == false)
                    _upgraders[upgrader.Team] = new List<IActorComponentUpgrader>();

                _upgraders[upgrader.Team].Add(upgrader);
            }
        }

        public void UpgradeActorComponents(TeamType teamType, IActor actor)
        {
            if (_upgraders.ContainsKey(teamType) == false)
                return;

            foreach (var upgrader in _upgraders[teamType])
                upgrader.UpgradeActorComponents(actor);
        }
    }
}