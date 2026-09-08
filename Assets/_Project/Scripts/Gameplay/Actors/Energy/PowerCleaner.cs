using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerCleaner : IActorComponent
    {
        private readonly IAdvancedPowerRegistry _powerRegistry;
        private readonly IActorData _data;
        private readonly ITeamable _teamable;

        public PowerCleaner(IAdvancedPowerRegistry powerRegistry, IActorData data, ITeamable teamable)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Power <= 0)
                throw new ArgumentOutOfRangeException(nameof(data.Power));

            _data = data;
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public Type KeyType => typeof(PowerCleaner);

        public void Disable() => 
            _powerRegistry.Release(_teamable.TeamType, _data);

        public void Enable() { }
    }
}
