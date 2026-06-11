using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Weapon : IWeapon
    {
        private readonly Timer _timer;

        private IProjectileController _projectileController;
        private int _currentNumberShells;

        public Weapon(IWeaponConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));

            _timer = new Timer(Config.RateShooting);
            _currentNumberShells = 0;
        }

        public IWeaponConfig Config { get; }

        public bool CanAttack { get; private set; }

        public void Init(IProjectileController projectileController)
        {
            _projectileController ??= projectileController ?? throw new ArgumentNullException(nameof(projectileController));
        }

        public void Enable()
        {
            CanAttack = true;

            Reload();
        }

        public void Update(float delta)
        {
            _timer.Tick(delta);

            if (_timer.IsTimeUp)
                CanAttack = true;
        }

        public void AttackTarget(ITarget target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (CanAttack == false)
                return;

            CanAttack = false;

            _projectileController.ShootMissile(target);

            if (--_currentNumberShells > 0)
            {
                _timer.SetWaitTime(Config.RateShooting);
            }
            else
            {
                Reload();
            }

            _timer.RestartTimer();
        }

        private void Reload()
        {
            _timer.SetWaitTime(Config.SpeedReload);
            _currentNumberShells = Config.NumberShells;
        }
    }
}