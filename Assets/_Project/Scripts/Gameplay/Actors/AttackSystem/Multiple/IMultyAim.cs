using BattleBase.Gameplay.Actors.AttackSystem.Aim;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public interface IMultyAim : IActorViewComponent
    {
        public IEnumerable<IAim> AdditionalAims { get; }
    }
}