using BattleBase.Gameplay.Actors.AttackSystem.Missiles;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Weapon : IWeapon
    {
        private readonly Timer _timer;

        private IMissileController _missileController;
        private int _currentNumberShells;

        public Weapon(IWeaponConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));

            _timer = new Timer(Config.RateShooting);
            _currentNumberShells = 0;
        }

        public IWeaponConfig Config { get; }

        public bool CanAttack { get; private set; }

        public void Init(IMissileController missileController)
        {
            _missileController ??= missileController ?? throw new ArgumentNullException(nameof(missileController));
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

            _missileController.ShootMissile(target);

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