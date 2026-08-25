using System;

namespace BattleBase.Gameplay.Actors.Movement.Hunt
{
    public class HuntMoverBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IHuntMover huntMover) == false)
                return;

            huntMover.Init(view);
        }
    }
}
