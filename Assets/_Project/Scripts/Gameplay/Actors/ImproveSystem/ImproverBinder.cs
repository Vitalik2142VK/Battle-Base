using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IImproverComponent improver) == false)
                return;

            improver.Init(actor);

            if (actor.TryGetComponent(out IActorSpawner spawner))
            {
                SpawnerImprover spawnerImprovement = new(spawner, improver);
                actor.AddComponent(spawnerImprovement);
            }

            if (actor.TryGetComponent(out IPowerGenerator powerGenerator))
            {
                PowerGeneratorImprover generatorImprovement = new(powerGenerator, improver);
                actor.AddComponent(generatorImprovement);
            }

            if (actor.TryGetComponent(out IMaterialCreator materialCreator)) 
            {
                MaterialCreatorImprover materialCreatorImprovement = new(materialCreator, improver);
                actor.AddComponent(materialCreatorImprovement);
            }
        }
    }
}