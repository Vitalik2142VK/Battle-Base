using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class ActorBinderRegistry : IActorBinderRegistry
    {
        private readonly List<IActorComponentBinder> _binds;

        public ActorBinderRegistry(IEnumerable<IActorComponentBinder> binds)
        {
            if (binds == null)
                throw new ArgumentNullException(nameof(binds));

            _binds = new List<IActorComponentBinder>(binds);
        }

        public void Bind(IActor actor, IActorView view)
        {
            foreach (var binder in _binds)
                binder.Bind(actor, view);
        }
    }
}
