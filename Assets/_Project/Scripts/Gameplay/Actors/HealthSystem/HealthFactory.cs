using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.DamageSystem.Modifiers;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthFactory : IComponentFactory
    {
        private readonly IDamageModifierFactory _factory;

        public HealthFactory(IDamageModifierFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public Type SourceType => typeof(HealthComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IHealthComponentSource healthSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IHealthComponentSource)}");

            IDamageModifier damageModifier = _factory.Create(healthSource);

            return new Health(healthSource.Config, damageModifier, healthSource.Type);
        }
    }
}
