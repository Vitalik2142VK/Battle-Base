using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class MultyAttacker : IMultyAttacker
    {
        private readonly List<IAttacker> _attackers;
        private readonly IAttacker _mainAttacker;

        public event Action TargetSelected;
        public event Action Attacked;
        public event Action AttackActivated;
        public event Action AttackDeactivated;

        public MultyAttacker(IAttacker mainAttacker)
        {
            _mainAttacker = mainAttacker ?? throw new ArgumentNullException(nameof(mainAttacker));
            _attackers = new List<IAttacker>() { mainAttacker };
        }

        public IEnumerable<IAttacker> Attackers => _attackers;

        public IWeaponConfig WeaponConfig => _mainAttacker.WeaponConfig;

        public ITarget CurrentTarget => _mainAttacker.CurrentTarget;

        public void Init(ITargetController targetController, IProjectileController projectileController) =>
            _mainAttacker.Init(targetController, projectileController);

        public void Enable()
        {
            foreach (var attacker in _attackers)
                attacker.Enable();

            _mainAttacker.TargetSelected += TargetSelected;
            _mainAttacker.Attacked += Attacked;
            _mainAttacker.AttackActivated += AttackActivated;
            _mainAttacker.AttackDeactivated += AttackDeactivated;
        }

        public void Disable()
        {
            _mainAttacker.TargetSelected -= TargetSelected;
            _mainAttacker.Attacked -= Attacked;
            _mainAttacker.AttackActivated -= AttackActivated;
            _mainAttacker.AttackDeactivated -= AttackDeactivated;

            foreach (var attacker in _attackers)
                attacker.Enable();
        }

        public void AddAttacker(IAttacker attacker)
        {
            if (attacker == null)
                throw new ArgumentNullException(nameof(attacker));

            _attackers.Add(attacker);
        }

        public void SetTargets(IEnumerable<ITarget> targets)
        {
            foreach (var attacker in _attackers)
                attacker.SetTargets(targets);
        }

        public void Update(float delta)
        {
            foreach (var attacker in _attackers)
                attacker.Update(delta);
        }

        public void Upgrade(IWeaponConfigModificator modificator)
        {
            foreach (var attacker in _attackers)
                attacker.Upgrade(modificator);
        }

        public void SetAim(bool isAiming) =>
            _mainAttacker.SetAim(isAiming);

        public void SetAttacking(bool isAttacking) => 
            _mainAttacker.SetAttacking(isAttacking);
    }
}