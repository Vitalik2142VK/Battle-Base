using BattleBase.Core;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorPool : IPool<Actor>
    {
        private readonly IFactory<Actor> _factory;
        private readonly Stack<Actor> _actors;
        private readonly int _size;

        private int _count;

        public ActorPool(IFactory<Actor> factory, int size)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _actors = new Stack<Actor>();
            _size = size;
        }

        public bool TryGive(out Actor actor)
        {
            actor = null;

            if (_actors.Count == 0 && _count >= _size)
                return false;

            actor = _actors.Count > 0 ? _actors.Pop() : Create();
            actor.Deactivated += OnReturn;

            return true;
        }

        private void OnReturn(Actor actor)
        {
            if (actor == null)
                return;

            //todo
            // === DEBUG START ===
            bool wasEnabled = actor.IsEnabled;
            TeamType teamBefore = actor.TeamType;
            // === DEBUG END ===

            actor.Deactivated -= OnReturn;
            actor.Disable();

            //todo
            // === DEBUG START ===
            if (wasEnabled == false)
                FiXiK.CustomLogger.XLogger.LogWarning($"[ActorPool] OnReturn on already disabled actor. Team={teamBefore}, Id={actor.Data?.Id}");

            if (actor.IsEnabled)
                FiXiK.CustomLogger.XLogger.LogWarning($"[ActorPool] Actor still IsEnabled=true after Disable() in OnReturn. Team={teamBefore}, Id={actor.Data?.Id}");

            if (teamBefore == TeamType.None)
                FiXiK.CustomLogger.XLogger.LogWarning($"[ActorPool] OnReturn with TeamType.None. Id={actor.Data?.Id}");
            // === DEBUG END ===

            _actors.Push(actor);
        }

        private Actor Create()
        {
            _count++;

            return _factory.Create();
        }
    }
}