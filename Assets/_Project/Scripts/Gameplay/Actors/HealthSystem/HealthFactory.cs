using BattleBase.Gameplay.Actors.HealthSystem;
using System;

namespace BattleBase.Gameplay.Actors
{
    public class HealthFactory : IComponentFactory
    {
        public Type SourceType => typeof(HealthComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IHealthComponentSource healthSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IHealthComponentSource)}");

            DamageModifier damageModifier = new();

            return new Health(healthSource.Config, damageModifier);
        }
    }
}
