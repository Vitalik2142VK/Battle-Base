using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerNotifier
    {
        public event Action<IActor> Spawned;
        public event Action SpawnStarted;
        public event Action SpawnCanceled;
        public event Action SpawnFinished;

        public bool IsInProcessSpawn { get; }
    }
}