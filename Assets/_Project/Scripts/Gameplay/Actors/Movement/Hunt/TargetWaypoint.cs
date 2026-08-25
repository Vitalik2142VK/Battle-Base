using BattleBase.Gameplay.Actors.DamageSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public class TargetWaypoint : IWaypoint
    {
        private readonly ITarget _target;
        private readonly Vector3 _offset;
        private readonly bool _isFixedY;

        public TargetWaypoint(ITarget target, Vector3 offset, bool isFixedY = true)
        {
            _target = target ?? throw new System.ArgumentNullException(nameof(target));
            _offset = offset;
            _isFixedY = isFixedY;
        }

        public Vector3 Position => _target.Position + GetCorrectOffset();

        private Vector3 GetCorrectOffset()
        {
            if (_isFixedY == false)
                return _offset;

            float offsetY = _offset.y - _target.Position.y;

            return new Vector3(_offset.x, offsetY, _offset.z);
        }
    }
}
