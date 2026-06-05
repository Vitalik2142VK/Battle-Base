using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface ITargetController : IUpdateable
    {
        public ITarget CurrentTarget { get; }

        public bool HasTarget { get; }

        public bool TryChangeTarget(ITarget newTarget);

        public void LoseTarget();
    }
}