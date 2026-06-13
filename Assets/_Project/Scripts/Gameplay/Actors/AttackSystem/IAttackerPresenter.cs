using BattleBase.Gameplay.Actors.DamageSystem;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttackerPresenter
    {
        public void SetTargets(IEnumerable<ITarget> targets);

        public void EstablishAimState(bool isAimed);
    }
}