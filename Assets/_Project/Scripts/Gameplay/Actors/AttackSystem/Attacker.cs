using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Attacker : IAttacker
    {
        private readonly IWeapon _weapon;

        private ITargetController _targetController;
        private bool _isAiming;
        private bool _isAttacking;

        public event Action<ITarget> TargetSelected;
        public event Action Attacked;
        public event Action AttackActivated;
        public event Action AttackDeactivated;

        public Attacker(IWeapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            _isAiming = false;
            _isAttacking = false;
        }

        public IWeaponConfig WeaponConfig => _weapon.Config;

        public void Init(ITargetController targetController, IProjectileController projectileController)
        {
            _targetController ??= targetController ?? throw new ArgumentNullException(nameof(targetController));

            _weapon.Init(projectileController);
        }

        public void Enable()
        {
            _weapon.Enable();
        }

        public void Disable()
        {
            _targetController.LoseTarget();
        }

        public void SetTargets(IEnumerable<ITarget> targets)
        {
            if (_targetController.TrySelectTarget(targets))
            {
                TargetSelected?.Invoke(_targetController.CurrentTarget);

                if (_isAttacking == false)
                    AttackActivated?.Invoke();
            }
        }

        public void Update(float delta)
        {
            _targetController.Update(delta);

            if (_targetController.HasTarget && _isAiming)
            {
                _weapon.Update(delta);

                if (_weapon.CanAttack)
                {
                    _weapon.AttackTarget(_targetController.CurrentTarget);

                    Attacked?.Invoke();
                }
            }

            if (_targetController.HasTarget == false && _isAttacking)
                AttackDeactivated?.Invoke();
        }

        public void SetAim(bool isAiming) =>
            _isAiming = isAiming;

        public void SetAttacking(bool isAttacking) =>
            _isAttacking = isAttacking;
    }
}