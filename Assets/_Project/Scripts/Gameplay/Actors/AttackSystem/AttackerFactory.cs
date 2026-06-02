using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerFactory : IComponentFactory
    {
        public Type SourceType => typeof(AttackComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IAttackComponentSource attackSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IAttackComponentSource)}");

            Weapon weapon = new(attackSource.Config);

            return new Attacker(weapon);
        }
    }
}
