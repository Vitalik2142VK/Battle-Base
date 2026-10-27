using BattleBase.Gameplay.Actors.Economy;
using System;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public class DeactivatorByEconomic : IDeactivator
    {
        private readonly IMaterialData _materialData;
        private readonly int _materialsForDeactivate;

        public DeactivatorByEconomic(IMaterialData materialData, int materialsForDeactivate)
        {
            if (materialsForDeactivate <= 0)
                throw new ArgumentOutOfRangeException(nameof(materialsForDeactivate));

            _materialData = materialData ?? throw new ArgumentNullException(nameof(materialData));
            _materialsForDeactivate = materialsForDeactivate;
        }

        public bool IsActive => _materialData.CurrentMaterials > _materialsForDeactivate;
    }
}