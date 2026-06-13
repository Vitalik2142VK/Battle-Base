using BattleBase.Gameplay.Actors.DamageSystem;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface ITargetController : IUpdateable
    {
        public ITarget CurrentTarget { get; }

        public bool HasTarget { get; }

        public bool TrySelectTarget(IEnumerable<ITarget> targets);

        public void LoseTarget();
    }
}