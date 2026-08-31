using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public class Weapon : IWeapon
    {
        private readonly Timer _timer;
        private readonly Damage _damage;
        private readonly ModifiedWeaponConfig _config;

        private IProjectileController _projectileController;
        private int _currentNumberShells;

        public Weapon(IWeaponConfig config)
        {
            if (config == null) 
                throw new ArgumentNullException(nameof(config));

            _config = new ModifiedWeaponConfig(config);
            _damage = new Damage(_config.DamageConfig);
            _timer = new Timer(_config.RateShooting);
            _currentNumberShells = 0;
        }

        public IWeaponConfig Config => _config;

        public bool CanAttack { get; private set; }

        public bool IsReloaded { get; private set; }

        public void Init(IProjectileController projectileController)
        {
            _projectileController ??= projectileController ?? throw new ArgumentNullException(nameof(projectileController));
        }

        public void Enable()
        {
            CanAttack = true;
            IsReloaded = false;

            Reload();
        }

        public void Update(float delta)
        {
            _timer.Tick(delta);

            if (IsReloaded && _timer.IsTimeUp)
                Reload();

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

            _projectileController.Shot(target, _damage);

            if (--_currentNumberShells > 0)
                _timer.SetWaitTime(Config.RateShooting);
            else
                BeginReaload();

            _timer.RestartTimer();
        }

        private void BeginReaload()
        {
            _timer.SetWaitTime(Config.SpeedReload);
            IsReloaded = true;
        }

        private void Reload()
        {
            _currentNumberShells = Config.NumberShells;
            IsReloaded = false;
        }

        public void Upgrade(IWeaponConfigModificator modificator) =>
            _config.Modify(modificator);
    }
}