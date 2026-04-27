using BattleBase.Gameplay.DamageSystem;
using BattleBase.Gameplay.HealthSystem;
using BattleBase.Gameplay.Movement;
using BattleBase.Gameplay.Weapons;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    //todo Split into view and model
    public class Unit : MonoBehaviour, IUnit
    {
        [SerializeField] private UnitConfig _config;
        [SerializeField] private Mover _mover;
        [SerializeField] private Tower _tower;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private WeaponView _weaponView;
        [SerializeField] private SideUnit _sideUnit;

        private IUnit _attackedUnit;
        private IHealth _health;
        private Weapon _weapon;
        private Transform _transform;

        public bool IsAttacking => _attackedUnit != null;

        private void Awake()
        {
            DamageModifier damageModifier = new DamageModifier();

            _transform = transform;
            _health = new Health(_config.HealthConfig, _healthBar, damageModifier);
            _weapon = new Weapon(_config.WeaponConfig, _weaponView, _tower, this);
            
            _mover.Init(_config.MovementConfig);
        }

        private void Start()
        {
            _health.Restore();
        }

        public Vector3 Position => _transform.position;

        public SideUnit SideUnit => _sideUnit;

        public event Action Destroyed;

        private void OnEnable()
        {
            _mover.Move();
        }

        private void OnDisable()
        {
            Destroyed?.Invoke();

            _weapon.Disable();

            if (_attackedUnit != null)
                _attackedUnit.Destroyed -= OnRemoveAttackedUnit;
        }

        public void TakeDamage(IDamage damage)
        {
            _health.TakeDamage(damage);

            if (_health.IsAlive == false)
                Destroy(gameObject);
        }

        public void AttackUnit(IUnit unit)
        {
            if (IsAttacking)
                return;

            _attackedUnit = unit ?? throw new ArgumentNullException(nameof(unit));
            _attackedUnit.Destroyed += OnRemoveAttackedUnit;

            _weapon.ShootUnit(_attackedUnit);
            _mover.Stop();
        }

        private void OnRemoveAttackedUnit()
        {
            _attackedUnit = null;
            _mover.Move();
        }
    }
}
