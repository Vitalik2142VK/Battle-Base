using BattleBase.Core;
using BattleBase.Gameplay.DamageSystem;
using BattleBase.Gameplay.HealthSystem;
using BattleBase.Gameplay.Movement;
using BattleBase.Gameplay.Weapons;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    //todo Split into view and model
    public class Unit : MonoBehaviour, IUnit, IPoolable<Unit>
    {
        [SerializeField] private UnitConfig _config;
        [SerializeField] private Mover _mover;
        [SerializeField] private Tower _tower;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private WeaponView _weaponView;

        private IUnit _attackedUnit;
        private IHealth _health;
        private Weapon _weapon;
        private Transform _transform;

        public bool IsAttacking => _attackedUnit != null;

        public Vector3 Position => _transform.position;

        public SideUnit SideUnit { get; private set; }

        public float ConstructionTime => _config.ConstructionTime;

        public event Action Destroyed;
        public event Action<Unit> Deactivated;

        private void Awake()
        {
            DamageModifier damageModifier = new();
            _healthBar.Init();
            _mover.Init(_config.MovementConfig);

            _transform = transform;
            _health = new Health(_config.HealthConfig, _healthBar, damageModifier);
            _weapon = new Weapon(_config.WeaponConfig, _weaponView, _tower, this);
        }

        private void OnEnable()
        {
            _weapon.Enable();
            _mover.Move();
            _health.Restore();
        }

        private void OnDisable()
        {
            Destroyed?.Invoke();

            _weapon.Disable();

            RemoveAttackedUnit();
        }

        public void SetSide(SideUnit side) => SideUnit = side;

        public void TakeDamage(IDamage damage)
        {
            _health.TakeDamage(damage);

            if (_health.IsAlive == false)
                Deactivated?.Invoke(this);
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

        public void SetMovePoint(Vector3 movePoint)
        {
            _mover.SetPointPosition(movePoint);
        }

        private void OnRemoveAttackedUnit()
        {
            RemoveAttackedUnit();

            _mover.Move();
        }

        private void RemoveAttackedUnit()
        {
            if (_attackedUnit != null)
            {
                _attackedUnit.Destroyed -= OnRemoveAttackedUnit;
                _attackedUnit = null;
            }
        }
    }
}
