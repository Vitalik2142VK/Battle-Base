using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapObjectRegistry : IMiniMapObjectRegistry
    {
        private readonly List<IMiniMapTrackable> _trackables = new();

        public event Action Changed;

        public IReadOnlyList<IMiniMapTrackable> Trackables => _trackables;

        public void Register(IMiniMapTrackable trackable)
        {
            if (trackable == null)
                throw new ArgumentNullException(nameof(trackable));

            if (_trackables.Contains(trackable))
                throw new InvalidOperationException("Attempt to re-register");

            _trackables.Add(trackable);

            Changed?.Invoke();
        }

        public void Unregister(IMiniMapTrackable trackable)
        {
            if (trackable == null)
                throw new ArgumentNullException(nameof(trackable));

            if (_trackables.Contains(trackable))
                _trackables.Remove(trackable);

            Changed?.Invoke();
        }
    }
}