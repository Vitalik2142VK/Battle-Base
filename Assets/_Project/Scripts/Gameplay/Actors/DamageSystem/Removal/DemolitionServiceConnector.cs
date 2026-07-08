using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Production.Factories;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class DemolitionServiceConnector : IActorComponentConnector
    {
        public void Connect(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IDemolition demolition) == false)
                return;

            if (actor.TryGetComponent(out IProductionService productionService))
            {
                DemolitionOptionsFactory factory = new(demolition);
                productionService.AddProductionFactory(factory);
            }
        }
    }
}