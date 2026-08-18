using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
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

        public event Action TargetSelected;
        public event Action Attacked;
        public event Action AttackActivated;
        public event Action AttackDeactivated;

        public Attacker(IWeapon weapon, ITargetFinderConfig targetFinderConfig)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            TargetFinderConfig = targetFinderConfig ?? throw new ArgumentNullException(nameof(targetFinderConfig));

            _isAiming = false;
            _isAttacking = false;
        }

        public Type KeyType => typeof(IAttacker);

        public IWeaponConfig WeaponConfig => _weapon.Config;

        public ITarget CurrentTarget => _targetController.CurrentTarget;

        public ITargetFinderConfig TargetFinderConfig { get; }

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
                TargetSelected?.Invoke();

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
            else
            {
                if (_weapon.IsReloaded)
                    _weapon.Update(delta);
            }

            if (_targetController.HasTarget == false && _isAttacking)
                AttackDeactivated?.Invoke();
        }

        public void Upgrade(IWeaponConfigModificator modificator) =>
            _weapon.Upgrade(modificator);

        public void SetAim(bool isAiming) =>
            _isAiming = isAiming;

        public void SetAttacking(bool isAttacking) =>
            _isAttacking = isAttacking;
    }
}