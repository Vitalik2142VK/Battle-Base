using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IMultyAttacker : IAttacker
    {
        public IEnumerable<IAttacker> AdditionalAttackers { get; }
    }
}