using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerRegistrator : IActorComponent
    {
        private readonly IAdvancedPowerRegistry _powerRegistry;
        private readonly IActorData _data;
        private readonly ITeamable _teamable;

        public PowerRegistrator(IAdvancedPowerRegistry powerRegistry, IActorData data, ITeamable teamable)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Power <= 0)
                throw new ArgumentOutOfRangeException(nameof(data.Power));

            _data = data;
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public Type KeyType => typeof(PowerRegistrator);

        public void Enable() =>
            _powerRegistry.Reserve(_teamable.TeamType, _data);

        public void Disable()
        {
            //todo
            // === DEBUG START ===
            if (_teamable.TeamType == TeamType.None)
                FiXiK.CustomLogger.XLogger.LogWarning($"[PowerCleaner] Disable() with TeamType.None (Power={_data.Power}). Release skipped to avoid KeyNotFoundException.");
            // === DEBUG END ===

            _powerRegistry.Release(_teamable.TeamType, _data);
    }
}
