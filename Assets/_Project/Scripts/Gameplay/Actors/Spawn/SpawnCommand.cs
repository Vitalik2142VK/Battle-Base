using BattleBase.Core;
using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class SpawnCommand : ICommand
    {
        private readonly IActorSpawner _actorSpawner;
        private readonly IActorData _actorData;

        public SpawnCommand(IActorSpawner actorSpawner, IActorData actorData)
        {
            _actorSpawner = actorSpawner ?? throw new ArgumentNullException(nameof(actorSpawner));
            _actorData = actorData ?? throw new ArgumentNullException(nameof(actorData));
        }

        public void Execute() =>
            _actorSpawner.SelectActorData(_actorData);
    }
}