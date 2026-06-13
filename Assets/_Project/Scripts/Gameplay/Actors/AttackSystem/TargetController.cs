using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils.Extensions;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class TargetController : ITargetController
    {
        private readonly IActorPosition _actorPosition;
        private readonly IWeaponRange _weaponRange;
        private readonly ITargetingProfile _targetingProfile;

        private ITarget _currentTarget;

        public TargetController(
            IActorPosition actorPosition,
            IWeaponRange weaponRange,
            ITargetingProfile targetingProfile)
        {
            _actorPosition = actorPosition ?? throw new ArgumentNullException(nameof(actorPosition));
            _weaponRange = weaponRange ?? throw new ArgumentNullException(nameof(weaponRange));
            _targetingProfile = targetingProfile ?? throw new ArgumentNullException(nameof(targetingProfile));
        }

        public ITarget CurrentTarget => _currentTarget;

        public bool HasTarget => _currentTarget != null;

        public bool TrySelectTarget(IEnumerable<ITarget> targets)
        {
            if (targets == null)
                throw new ArgumentNullException(nameof(targets));

            if (_currentTarget == null)
            {
                if (TryFindTarget(targets, out ITarget newTarget))
                {
                    _currentTarget = newTarget;
                    _currentTarget.Destroyed += OnLoseTarget;

                    return true;
                }
            }

            return false;
        }

        public void Update(float _)
        {
            if (_currentTarget == null)
                return;

            if (_actorPosition.Position.IsWithinDistance(_currentTarget.Position, _weaponRange.Range) == false)
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

        private bool TryFindTarget(IEnumerable<ITarget> targets, out ITarget newTarget)
        {
            foreach (var priorityActorType in _targetingProfile.PriorityActorTypes)
            {
                foreach (var target in targets)
                {
                    if (priorityActorType.ActorMask.Contains(target.ActorMask))
                    {
                        newTarget = target;

                        return true;
                    }
                }
            }

            foreach (var target in targets)
            {
                if (target.ActorMask.ContainsAny(_targetingProfile.NotAttacked) == false)
                {
                    newTarget = target;

                    return true;
                }
            }

            newTarget = null;

            return false;
        }

        private void OnLoseTarget() =>
            LoseTarget();
    }
}