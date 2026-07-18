using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IMultyAttackerViewComponent : IActorViewComponent
    {
        public IEnumerable<IAttackerViewComponent> AdditionalAttackerView { get; }
    }
}