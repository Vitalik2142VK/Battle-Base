using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerRegistry : IAdvancedPowerRegistry
    {
        private readonly Dictionary<TeamType, Power> _powers;

        public PowerRegistry(IPowerConfig powerConfig)
        {
            _powers = new Dictionary<TeamType, Power>
            {
                { TeamType.Player, new Power(powerConfig) },
                { TeamType.Enemy, new Power(powerConfig) },
            };
        }

        public void AddCapacity(TeamType team, int capacity) =>
            _powers[team].AddCapacity(capacity);

        public void ReduceCapacity(TeamType team, int capacity) =>
            _powers[team].ReduceCapacity(capacity);

        public bool TryReserve(TeamType team, int power) =>
            _powers[team].TryReserve(power);

        public IPowerData GetPowerData(TeamType team) =>
            _powers[team];

        public void Release(TeamType team, IActorData actorData)
        {
            if (actorData == null)
                throw new System.ArgumentNullException(nameof(actorData));

            _powers[team].Release(actorData.Power);
        }
    }
}
