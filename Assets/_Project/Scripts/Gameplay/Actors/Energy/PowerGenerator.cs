using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGenerator : IPowerGenerator
    {
        private readonly List<IPowerByRank> _addedPowerByRank;
        private readonly IAdvancedPowerRegistry _powerRegistry;

        private ITeamable _teamable;
        private int _currentRank;

        public PowerGenerator(IEnumerable<IPowerByRank> addedPowerByRank, IAdvancedPowerRegistry powerRegistry)
        {
            if (addedPowerByRank == null)
                throw new ArgumentNullException(nameof(addedPowerByRank));

            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
            _addedPowerByRank = new List<IPowerByRank>(addedPowerByRank);
            _currentRank = 0;
        }

        public bool CanIncreasePower => _currentRank >= _addedPowerByRank.Count;

        public void Init(ITeamable teamable)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public void Enable()
        {
            IncreasePower();
        }

        public void Disable()
        {
            _currentRank = 0;
        }

        public void IncreasePower()
        {
            if (CanIncreasePower == false)
                return;

            IPowerByRank powerByRank = _addedPowerByRank[_currentRank++];
            _powerRegistry.AddCapacity(_teamable.TeamType, powerByRank.AddedPower);
        }
    }
}
