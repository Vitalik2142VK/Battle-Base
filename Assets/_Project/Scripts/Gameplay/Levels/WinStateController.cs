using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Types;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Levels
{
    public class WinStateController : IWinStateController, IDisposable
    {
        private List<Actor> _bases;

        public WinStateController()
        {
            _bases = new List<Actor>();
        }

        public event Action<bool> Winned;

        public void AddBase(Actor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actor.View is Base == false)
                throw new InvalidOperationException($"{actor} must contain {nameof(Base)}");

            _bases.Add(actor);
            actor.Deactivated += OnDestroyBase;
        }

        public void Dispose()
        {
            foreach (var actor in _bases)
                actor.Deactivated -= OnDestroyBase;
        }

        private void OnDestroyBase(Actor destroyedBase)
        {
            bool isWin = destroyedBase.TeamType != TeamType.Player;
            destroyedBase.Disable();

            Dispose();

            Winned?.Invoke(isWin);
        }
    }
}