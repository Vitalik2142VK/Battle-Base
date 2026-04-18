using BattleBase.Gameplay.DamageSystem;
using BattleBase.Gameplay.Units;
using System;
using System.Collections;
using UnityEngine;

namespace BattleBase.Gameplay.Weapons
{
    public class Weapon : IWeapon
    {
        private readonly IWeaponConfig _config;
        private readonly IWeaponView _weaponView;
        private readonly ITower _tower;
        private readonly IDamage _damage;
        private readonly MonoBehaviour _unit;
        private readonly WaitForSeconds _waitShoot;
        private readonly WaitForSeconds _waitReload;

        private IUnit _currentUnit;
        private Coroutine _coroutine;
        private int _currentNumberShells;

        public Weapon(IWeaponConfig config, IWeaponView weaponView, ITower tower, MonoBehaviour monoBehaviour)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _weaponView = weaponView ?? throw new ArgumentNullException(nameof(weaponView));
            _tower = tower ?? throw new ArgumentNullException(nameof(tower));
            _unit = monoBehaviour != null ? monoBehaviour : throw new ArgumentNullException(nameof(monoBehaviour));
            _damage = new Damage(_config.DamageConfig);
            _waitShoot = new WaitForSeconds(_config.RateShooting);
            _waitReload = new WaitForSeconds(_config.SpeedReload);

            _tower.Aimed += OnShoot;
            _currentNumberShells = _config.NumberShells;
        }

        public void ShootUnit(IUnit unit)
        {
            if (_currentUnit != null)
                _currentUnit.Destroyed -= OnRemoveTarget;

            _currentUnit = unit ?? throw new ArgumentNullException(nameof(unit));
            _currentUnit.Destroyed += OnRemoveTarget;

            _tower.TakeAim(unit);
        }

        public void Disable()
        {
            _tower.Aimed -= OnShoot;

            if (_currentUnit != null)
                _currentUnit.Destroyed -= OnRemoveTarget;

            StopCoroutine();
        }

        private void OnShoot(bool isAimed)
        {
            if (isAimed)
                _coroutine = _unit.StartCoroutine(Shot());
            else
                StopCoroutine();
        }

        private void OnRemoveTarget()
        {
            _currentUnit.Destroyed -= OnRemoveTarget;
            _currentUnit = null;
        }

        private void StopCoroutine()
        {
            if (_coroutine != null)
            {
                _unit.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator Shot()
        {
            while (_unit.gameObject.activeSelf && _currentUnit != null)
            {
                _currentNumberShells = _config.NumberShells;

                while (_currentNumberShells > 0)
                {
                    if (_currentUnit == null)
                        yield break;

                    _weaponView.PlayShot();
                    _currentUnit.TakeDamage(_damage);

                    _currentNumberShells--;

                    yield return _waitShoot;
                }

                yield return _waitReload;
            }

            _coroutine = null;
        }
    }
}