using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCreatorBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IMaterialCreator materialCreator) == false)
                return;

            materialCreator.Init(actor);
        }
    }
}