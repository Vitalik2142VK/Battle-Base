using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGeneratorFactory : IComponentFactory
    {
        private readonly IAdvancedPowerRegistry _powerRegistry;

        public PowerGeneratorFactory(IAdvancedPowerRegistry powerRegistry)
        {
            _powerRegistry = powerRegistry ?? throw new ArgumentNullException(nameof(powerRegistry));
        }

        public Type SourceType => typeof(PowerGeneratorSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IPowerGeneratorSource powerGeneratorSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IPowerGeneratorSource)}");

            return new PowerGenerator(powerGeneratorSource.AddedPowerByRank, _powerRegistry);
        }
    }
}
