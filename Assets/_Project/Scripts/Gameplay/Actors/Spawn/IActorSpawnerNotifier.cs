using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerNotifier
    {
        public event Action<IActor> Spawned;

        public bool IsInProcessSpawn { get; }
    }
}