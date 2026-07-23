using System;
using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public class EntityTracker : IEntityTracker, IDisposable
    {
        private readonly ITrackable _trackable;
        private readonly IEntitySizeTracker _sizeTracker;
        private readonly IEntityPositionTracker _positionTracker;
        private readonly IEntityRotationTracker _rotationTracker;

        public EntityTracker(
            ITrackable trackable, 
            IEntitySizeTracker sizeTracker,
            IEntityPositionTracker positionTracker,
            IEntityRotationTracker rotationTracker)
        {
            _trackable = trackable ?? throw new ArgumentNullException(nameof(trackable));
            _sizeTracker = sizeTracker ?? throw new ArgumentNullException(nameof(sizeTracker));
            _positionTracker = positionTracker ?? throw new ArgumentNullException(nameof(positionTracker));
            _rotationTracker = rotationTracker ?? throw new ArgumentNullException(nameof(rotationTracker));

            _trackable.Deactivated += OnDeactivated;
            _trackable.ColorChanged += OnColorChanged;
            _sizeTracker.Changed += OnSizeChanged;
            _positionTracker.Changed += OnPositionChanged;
            _rotationTracker.Changed += OnRotationChanged;
        }

        public event Action<IEntityTracker> Disposed;
        public event Action<IEntityTracker> ColorChanged;
        public event Action<IEntityTracker> SizeChanged;
        public event Action<IEntityTracker> PositionChanged;
        public event Action<IEntityTracker> RotationChanged;

        public Color Color => _trackable.Color;

        public Vector2 WorldSize => _sizeTracker.WorldSize;

        public Vector3 WorldPosition => _positionTracker.WorldPosition;

        public float RotationY => _rotationTracker.RotationY;

        public void Dispose()
        {
            _trackable.Deactivated -= OnDeactivated;
            _trackable.ColorChanged -= OnColorChanged;
            _sizeTracker.Changed -= OnSizeChanged;
            _positionTracker.Changed -= OnPositionChanged;
            _rotationTracker.Changed -= OnRotationChanged;

            _sizeTracker.Dispose();
            _positionTracker.Dispose();
            _rotationTracker.Dispose();

            Disposed?.Invoke(this);
        }

        private void OnDeactivated(ITrackable entity) =>
            Dispose();

        private void OnColorChanged(ITrackable entity) =>
            ColorChanged?.Invoke(this);

        private void OnSizeChanged() =>
            SizeChanged?.Invoke(this);

        private void OnPositionChanged() =>
            PositionChanged?.Invoke(this);

        private void OnRotationChanged() =>
            RotationChanged?.Invoke(this);
    }
}