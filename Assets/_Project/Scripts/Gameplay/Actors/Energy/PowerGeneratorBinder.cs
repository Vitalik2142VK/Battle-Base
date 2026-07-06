using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGeneratorBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IPowerGenerator powerGenerator) == false)
                return;

            powerGenerator.Init(actor);
        }
    }
}
