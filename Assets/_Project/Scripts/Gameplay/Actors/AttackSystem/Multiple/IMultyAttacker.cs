using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public interface IMultyAttacker : IAttacker
    {
        public IEnumerable<IAttacker> AdditionalAttackers { get; }
    }
}