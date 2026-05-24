using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class LoopActorSpawner : ActorSpawner
    {
        private readonly IActorData _firstActorData;

        public LoopActorSpawner(IEnumerable<IActorData> actorsToCreate, IActorSpawnService actorSpawnService)
            : base(actorsToCreate, actorSpawnService)
        {
            if (actorsToCreate == null)
                throw new ArgumentNullException(nameof(actorsToCreate));

            _firstActorData = actorsToCreate.FirstOrDefault() 
                ?? throw new InvalidOperationException($"{nameof(actorsToCreate)} is empty");
        }

        public override void Enable()
        {
            base.Enable();

            SelectActorData(_firstActorData);
        }

        protected override void ActionOnSpawned()
        {
            Timer.RestartTimer();
        }
    }
}