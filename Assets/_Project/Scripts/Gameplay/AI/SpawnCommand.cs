using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.AI
{
    public class SpawnCommand : ICommand
    {
        private readonly IActorSpawner _actorSpawner;
        private readonly IActorData _actorData;
        private readonly int _count;

        public SpawnCommand(IActorSpawner actorSpawner, IActorData actorData, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            _actorSpawner = actorSpawner ?? throw new ArgumentNullException(nameof(actorSpawner));
            _actorData = actorData ?? throw new ArgumentNullException(nameof(actorData));
            _count = ++count;
        }

        public void Execute()
        {
            for (int i = 0; i < _count; i++)
                _actorSpawner.SelectActorData(_actorData);
        }
    }
}