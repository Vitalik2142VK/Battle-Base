using BattleBase.Gameplay.Actors.AttackSystem.Multiple;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
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

            MultyAttacker multyAttacker = null;

            foreach (var config in attackSource.Configs)
            {
                Attacker attacker = CreateAttacker(config, attackSource);

                if (attackSource.IsSingle)
                    return attacker;
                
                if (multyAttacker == null)
                    multyAttacker = new MultyAttacker(attacker);
                else
                    multyAttacker.AddAttacker(attacker);
            }

            return multyAttacker;
        }

        private Attacker CreateAttacker(IWeaponConfig weaponConfig, ITargetFinderConfig targetConfig)
        {
            Weapon weapon = new(weaponConfig);

            return new Attacker(weapon, targetConfig);
        }
    }
}
