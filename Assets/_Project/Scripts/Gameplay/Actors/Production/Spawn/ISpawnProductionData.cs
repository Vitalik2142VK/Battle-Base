using System;

namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public interface ISpawnProductionData
    {
        public event Action DataChanged;

        public IActorData ActorData { get; }

        public float ConstructionProgress { get; }

        public int Count { get; }

        public bool IsInProcessSpawn { get; }
    }
}