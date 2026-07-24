using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGenerator : IPowerGenerator
    {
        private readonly List<IPowerByRank> _addedPowerByRank;
        private readonly IAdvancedPowerRegistry _powerRegistry;

        private ITeamable _teamable;
        private int _powerCount;
        private int _currentRank;

        public event Action PowerChanged;

        public PowerGenerator(IEnumerable<IPowerByRank> addedPowerByRank, IAdvancedPowerRegistry powerRegistry)
        {
            if (addedPowerByRank == null)
                throw new ArgumentNullException(nameof(addedPowerByRank));

            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
            _addedPowerByRank = new List<IPowerByRank>(addedPowerByRank);
            _powerCount = 0;
            _currentRank = 0;
        }

        public Type KeyType => typeof(IPowerGenerator);

        public bool CanIncreasePower => _currentRank < _addedPowerByRank.Count;

        public int PowerCount => _powerCount;

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
            _powerRegistry.ReduceCapacity(_teamable.TeamType, _powerCount);
            _powerCount = 0;
            _currentRank = 0;
        }

        public void IncreasePower()
        {
            if (CanIncreasePower == false)
                return;

            IPowerByRank powerByRank = _addedPowerByRank[_currentRank++];
            _powerRegistry.AddCapacity(_teamable.TeamType, powerByRank.AddedPower);
            _powerCount += powerByRank.AddedPower;

            PowerChanged?.Invoke();
        }
    }
}
