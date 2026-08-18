using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerNotifier
    {
        public event Action<IActor> Spawned;
        public event Action SpawnStarted;
        public event Action SpawnCancled;
        public event Action SpawnFinished;

        public bool IsInProcessSpawn { get; }
    }
}