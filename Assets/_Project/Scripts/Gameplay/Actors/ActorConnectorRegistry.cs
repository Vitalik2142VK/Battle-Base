using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class ActorConnectorRegistry : IActorConnectorRegistry
    {
        private readonly List<IActorComponentConnector> _connectors;

        public ActorConnectorRegistry(IEnumerable<IActorComponentConnector> connectors)
        {
            if (connectors == null)
                throw new ArgumentNullException(nameof(connectors));

            _connectors = new List<IActorComponentConnector>(connectors);
        }

        public void Connect(IActor actor)
        {
            foreach (var connector in _connectors)
                connector.Connect(actor);
        }
    }
}
