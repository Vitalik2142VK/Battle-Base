using BattleBase.Core;
using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class CancelSpawnCommand : ICommand
    {
        private readonly IActorSpawner _actorSpawner;
        private readonly IActorData _actorData;

        public CancelSpawnCommand(IActorSpawner actorSpawner, IActorData actorData)
        {
            _actorSpawner = actorSpawner ?? throw new ArgumentNullException(nameof(actorSpawner));
            _actorData = actorData ?? throw new ArgumentNullException(nameof(actorData));
        }

        public void Execute() =>
            _actorSpawner.CancelSpawnActor(_actorData);
    }
}