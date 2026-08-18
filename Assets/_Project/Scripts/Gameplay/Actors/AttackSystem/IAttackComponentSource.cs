using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackComponentSource : IComponentSource, ITargetFinderConfig
    {
        public IEnumerable<IWeaponConfig> Configs { get; }

        public bool IsSingle { get; }
    }
}