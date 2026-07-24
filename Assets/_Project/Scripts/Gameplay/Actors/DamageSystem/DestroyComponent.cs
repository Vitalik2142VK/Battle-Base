using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public class DestroyComponent : IDestroyComponent
    {
        private readonly List<IDestroyableEvent> _destroyableEvents;

        private bool _isActive;

        public event Action Destroyed;

        public DestroyComponent(IDestroyableEvent destroyableEvent)
        {
            if (destroyableEvent == null)
                throw new ArgumentNullException(nameof(destroyableEvent));

            _destroyableEvents = new List<IDestroyableEvent>
            {
                destroyableEvent
            };

            _isActive = false;
        }

        public Type KeyType => typeof(IDestroyComponent);

        public void Enable()
        {
            _isActive = true;

            foreach (var destroyableEvent in _destroyableEvents)
                destroyableEvent.Destroyed += OnDestroy;
        }

        public void Disable()
        {
            _isActive = false;

            foreach (var destroyableEvent in _destroyableEvents)
                destroyableEvent.Destroyed -= OnDestroy;
        }

        public void AddDestroyableEvent(IDestroyableEvent destroyableEvent)
        {
            if (destroyableEvent == null)
                throw new ArgumentNullException(nameof(destroyableEvent));

            _destroyableEvents.Add(destroyableEvent);

            if (_isActive)
                destroyableEvent.Destroyed += OnDestroy;
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }
    }
}