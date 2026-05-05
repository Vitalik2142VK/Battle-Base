using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Weapons;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    //todo Split into view and model
    public class Unit : MonoBehaviour, IUnit, IPoolable<Unit>
    {
        [SerializeField] private ActorConfig _config;
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

        public TeamType TeamType { get; private set; }

        public float ConstructionTime => 0;

        public event Action Destroyed;
        public event Action<Unit> Deactivated;

        private void Awake()
        {
            DamageModifier damageModifier = new();

            _transform = transform;
            //_health = new Health(_config.HealthConfig, damageModifier);
            //_weapon = new Weapon(_config.WeaponConfig, _weaponView, _tower, this);

            //_healthBar.Init(_health);
            //_mover.Init(_config.MovementConfig);
        }

        private void OnEnable()
        {
            _weapon.Enable();
            _mover.Move();
        }

        private void OnDisable()
        {
            Destroyed?.Invoke();

            _weapon.Disable();

            RemoveAttackedUnit();
        }

        public void SetSide(TeamType team) => TeamType = team;

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
            //_attackedUnit.Destroyed += OnRemoveAttackedUnit;

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
                //_attackedUnit.Destroyed -= OnRemoveAttackedUnit;
                _attackedUnit = null;
            }
        }
    }
}
