using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.Weapons
{
    public class Weapon : IWeapon
    {
        private readonly IWeaponConfig _config;
        private readonly Timer _timer;

        private ITarget _currentTarget;
        private int _currentNumberShells;
        private bool _isShotActive;

        public event Action<ITarget> TargetSelected;
        public event Action<ITarget, IDamageConfig> TargetFired;
        public event Action Shooted;
        public event Action TargetReseted;

        public Weapon(IWeaponConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _timer = new Timer(_config.RateShooting);
        }

        public void Enable()
        {
            _currentTarget = null;
            _isShotActive = false;

            Reload();
        }

        public void Disable()
        {
            if (_currentTarget != null)
                _currentTarget.Destroyed -= OnResetTurget;
        }

        public void Update(float delta)
        {
            if (_currentTarget == null || _isShotActive == false)
                return;

            _timer.SkipTick(delta);

            if (_timer.IsTimeUp)
                ShootTarget();
        }

        public void SetTarget(ITarget target)
        {
            if (_currentTarget != null)
                _currentTarget.Destroyed -= OnResetTurget;

            _currentTarget = target ?? throw new ArgumentNullException(nameof(target));
            _currentTarget.Destroyed += OnResetTurget;

            TargetSelected?.Invoke(_currentTarget);
        }

        public void Shoot()
        {
            _isShotActive = true;

            if (_timer.IsTimeUp)
                ShootTarget();
        }

        public void ShootStop()
        {
            _isShotActive = false;
        }

        private void ShootTarget()
        {
            TargetFired?.Invoke(_currentTarget, _config.DamageConfig);
            Shooted?.Invoke();

            if (_currentNumberShells > 0)
            {
                _timer.SetWaitTime(_config.RateShooting);
                _currentNumberShells--;
            }
            else
            {
                Reload();
            }

            _timer.RestartTimer();

        }

        private void Reload()
        {
            _timer.SetWaitTime(_config.SpeedReload);
            _currentNumberShells = _config.NumberShells;
        }

        private void OnResetTurget()
        {
            TargetReseted?.Invoke();
        }
    }
}