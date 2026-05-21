using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerEvents
    {
        public event Action<Actor> Spawned;
    }
}