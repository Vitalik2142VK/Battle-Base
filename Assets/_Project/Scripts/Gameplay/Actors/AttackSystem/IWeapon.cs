using BattleBase.Gameplay.Actors.AttackSystem.Missiles;
using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IWeapon : IUpdateable
    {
        public IWeaponConfig Config { get; }

        public bool CanAttack { get; }

        public void Init(IMissileController missileController);

        public void Enable();

        public void AttackTarget(ITarget target);
    }
}