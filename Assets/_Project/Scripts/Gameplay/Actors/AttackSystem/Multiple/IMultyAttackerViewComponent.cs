using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public interface IMultyAttackerViewComponent : IActorViewComponent
    {
        public IEnumerable<IAttackerViewComponent> AdditionalAttackerView { get; }
    }
}