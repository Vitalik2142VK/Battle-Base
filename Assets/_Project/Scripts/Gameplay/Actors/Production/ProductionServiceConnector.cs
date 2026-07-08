using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production.Factories;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionServiceConnector : IActorComponentConnector
    {
        private readonly List<IProductionOptionsFactory> _factories;

        private IActor _actor;

        public ProductionServiceConnector()
        {
            _factories = new List<IProductionOptionsFactory>();
        }

        public void Connect(IActor actor)
        {
            _actor = actor ?? throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IProductionService productionService) == false)
                return;

            AddActorSpawnOptionsFactory();
            AddPowerGeneratorOptionsFactory();

            foreach (var factory in _factories)
                productionService.AddProductionFactory(factory);

            _factories.Clear();
            _actor = null;
        }

        private void AddActorSpawnOptionsFactory()
        {
            if (_actor.TryGetComponent(out IActorSpawner spawner) == false)
                return;

            if (_actor.TryGetComponent(out ISpawnerImprover spawnerImprovement))
                _factories.Add(new ImproveActorSpawnOptionsFactory(spawner, spawnerImprovement));
            else
                _factories.Add(new ActorSpawnOptionsFactory(spawner));
        }

        private void AddPowerGeneratorOptionsFactory()
        {
            if (_actor.TryGetComponent(out IPowerGenerator _) == false)
                return;

            if (_actor.TryGetComponent(out IPowerGeneratorImprover powerGeneratorImprovement))
                _factories.Add(new ImprovePowerGeneratorOptionsFactory(powerGeneratorImprovement));
        }
    }
}