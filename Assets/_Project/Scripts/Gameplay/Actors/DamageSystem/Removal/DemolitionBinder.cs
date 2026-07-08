using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production;
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

            IProductionData currentData;

            if (actor.TryGetComponent(out IImprover improvement))
                currentData = improvement.Data;
            else
                currentData = actor.Data;

            demolition.Init(currentData);
        }
    }
}