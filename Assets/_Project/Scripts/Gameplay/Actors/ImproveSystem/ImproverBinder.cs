using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverBinder : IActorComponentBinder
    {
        private readonly IMaterialRegistry _materialRegistry;

        public ImproverBinder(IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IImprover improvement) == false)
                return;

            if (actor.TryGetComponent(out IActorSpawner spawner))
            {
                SpawnerImprover spawnerImprovement = new(spawner, improvement, _materialRegistry, actor);
                actor.AddComponent(spawnerImprovement);
            }

            if (actor.TryGetComponent(out IPowerGenerator powerGenerator))
            {
                PowerGeneratorImprover generatorImprovement = new(
                    powerGenerator, 
                    improvement, 
                    _materialRegistry, 
                    actor);
                actor.AddComponent(generatorImprovement);
            }

            if (actor.TryGetComponent(out IMaterialCreator materialCreator)) 
            {
                MaterialCreatorImprover materialCreatorImprovement = new(
                    materialCreator, 
                    improvement, 
                    _materialRegistry, 
                    actor);
                actor.AddComponent(materialCreatorImprovement);
            }
        }
    }
}