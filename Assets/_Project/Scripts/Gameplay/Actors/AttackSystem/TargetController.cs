using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class TargetController : ITargetController
    {
        private readonly IActorPosition _actorPosition;
        private readonly IWeaponRange _weaponRange;

        private ITarget _currentTarget;

        public TargetController(IActorPosition actorPosition, IWeaponRange weaponRange)
        {
            _actorPosition = actorPosition ?? throw new ArgumentNullException(nameof(actorPosition));
            _weaponRange = weaponRange ?? throw new ArgumentNullException(nameof(weaponRange));
        }

        public ITarget CurrentTarget => _currentTarget;

        public bool HasTarget => _currentTarget != null;

        public bool TryChangeTarget(ITarget newTarget)
        {
            if (newTarget == null)
                throw new ArgumentNullException(nameof(newTarget));

            if (_currentTarget == null)
            {
                _currentTarget = newTarget;

                _currentTarget.Destroyed += OnLoseTarget;

                return true;
            }

            return false;
        }

        public void Update(float _)
        {
            if (_currentTarget == null)
                return;

            if (VectorTool.IsWithinDistance(
                _actorPosition.Position, 
                _currentTarget.Position, 
                _weaponRange.Range) == false)
            {
                LoseTarget();
            }
        }

        public void LoseTarget()
        {
            if (_currentTarget == null)
                return;

            _currentTarget.Destroyed -= OnLoseTarget;

            _currentTarget = null;
        }

        private void OnLoseTarget() =>
            LoseTarget();
    }
}