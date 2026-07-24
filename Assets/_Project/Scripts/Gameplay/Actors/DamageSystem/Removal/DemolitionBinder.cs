using BattleBase.Gameplay.Actors.ImproveSystem;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class DemolitionBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IDemolition demolition) == false)
                return;

            actor.TryGetComponent(out IImproverComponent improver);

            PriceCounterDemolition priceCounter = new(demolition.Data, actor.Data, improver);

            demolition.Init(priceCounter, actor);
        }
    }
}