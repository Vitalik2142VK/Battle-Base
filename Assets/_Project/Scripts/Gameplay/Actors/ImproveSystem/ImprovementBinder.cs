using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImprovementBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IImprovement improvement) == false)
                return;

            if (actor.TryGetComponent(out IActorSpawner spawner))
            {
                SpawnerImprovement spawnerImprovement = new(spawner, improvement);
                spawnerImprovement.Init(actor.Data);
                actor.AddComponent(spawnerImprovement);
            }

            if (actor.TryGetComponent(out IPowerGenerator powerGenerator))
            {
                PowerGeneratorImprovement generatorImprovement = new(powerGenerator, improvement);
                generatorImprovement.Init(actor.Data);
                actor.AddComponent(generatorImprovement);
            }
        }
    }
}