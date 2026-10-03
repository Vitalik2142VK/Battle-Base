using BattleBase.Gameplay.Actors.Economy;
using System;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public class EconomicDeactivator : IDeactivator
    {
        private readonly IMaterialData _materialData;
        private readonly int _materialsForDeactivate;

        public EconomicDeactivator(IMaterialData materialData, int materialsForDeactivate)
        {
            if (materialsForDeactivate <= 0)
                throw new ArgumentOutOfRangeException(nameof(materialsForDeactivate));

            _materialData = materialData ?? throw new ArgumentNullException(nameof(materialData));
            _materialsForDeactivate = materialsForDeactivate;
        }

        public bool IsActive => _materialData.CurrentMaterials > _materialsForDeactivate;
    }
}