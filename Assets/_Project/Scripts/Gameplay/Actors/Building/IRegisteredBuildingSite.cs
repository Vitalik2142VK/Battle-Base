using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IRegisteredBuildingSite
    {
        public event Action ActorMissing;

        public int NumberLine { get; }

        public bool HasBuilding { get; }

        public bool IsUnderConstruction { get; }

        public bool TryGetProductionStorage(out IProductionStorage productionService);
    }
}