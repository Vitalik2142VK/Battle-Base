using System;

namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public interface ISpawnProductionOption : IProductionOption
    {
        public ISpawnProductionData SpawnData { get; }

        public void CancelSpawn();
    }
}