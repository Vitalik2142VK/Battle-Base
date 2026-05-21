using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IActorSpawner spawner) &&
                view.TryGetViewComponent(out IActorViewSpawner spawnerView))
            {
                ActorSpawnerPresenter presenter = new(spawner);

                spawner.Init(actor);
                spawnerView.Init(presenter, spawner);
            }
            else
            {
                return;
            }
        }
    }
}