using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
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
            _attackers = new List<IAttacker>();
        }

        public Type KeyType => typeof(IMultyAttacker);

        public IEnumerable<IAttacker> AdditionalAttackers => _attackers;

        public IWeaponConfig WeaponConfig => _mainAttacker.WeaponConfig;

        public ITarget CurrentTarget => _mainAttacker.CurrentTarget;

        public ITargetFinderConfig TargetFinderConfig => _mainAttacker.TargetFinderConfig;

        public void Init(ITargetController targetController, IProjectileController projectileController) => 
            _mainAttacker.Init(targetController, projectileController);

        public void Enable()
        {
            _mainAttacker.Enable();

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

            _mainAttacker.Disable();

            foreach (var attacker in _attackers)
                attacker.Disable();
        }

        public void AddAttacker(IAttacker attacker)
        {
            if (attacker == null)
                throw new ArgumentNullException(nameof(attacker));

            _attackers.Add(attacker);
        }

        public void SetTargets(IEnumerable<ITarget> targets)
        {
            _mainAttacker.SetTargets(targets);

            foreach (var attacker in _attackers)
                attacker.SetTargets(targets);
        }

        public void Update(float delta)
        {
            _mainAttacker.Update(delta);

            foreach (var attacker in _attackers)
                attacker.Update(delta);
        }

        public void Upgrade(IWeaponConfigModificator modificator)
        {
            _mainAttacker.Upgrade(modificator);

            foreach (var attacker in _attackers)
                attacker.Upgrade(modificator);
        }

        public void SetAim(bool isAiming) =>
            _mainAttacker.SetAim(isAiming);

        public void SetAttacking(bool isAttacking) => 
            _mainAttacker.SetAttacking(isAttacking);
    }
}