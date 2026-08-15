using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IRegisteredBuildingSite
    {
        public event Action<IRegisteredBuildingSite> ActorMissing;
        public event Action StateChanged;

        public string CurrentActorId { get; }

        public int BuildingSiteId { get; }

        public int NumberLine { get; }

        public bool HasBuilding { get; }

        public bool IsConstruction { get; }

        public bool TryGetProductionStorage(out IProductionStorage productionService);

        public void Select();

        public void Unselect();
    }
}