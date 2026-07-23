using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class Material : IMaterialData
    {
        private readonly IMaterialConfig _materialConfig;

        public event Action DataChanged;

        public Material(IMaterialConfig materialConfig)
        {
            _materialConfig = materialConfig ?? throw new ArgumentNullException(nameof(materialConfig));

            CurrentMaterials = _materialConfig.StartMaterialsCount;
        }

        public int CurrentMaterials { get; private set; }

        public void AddMaterials(int materials)
        {
            if (materials <= 0)
                throw new ArgumentOutOfRangeException(nameof(materials));

            if (CurrentMaterials == _materialConfig.MaxCapacity)
                return;

            CurrentMaterials += materials;

            if (CurrentMaterials > _materialConfig.MaxCapacity)
                CurrentMaterials = _materialConfig.MaxCapacity;

            DataChanged?.Invoke();
        }

        public bool TrySpend(int materials)
        {
            if (materials < 0)
                throw new ArgumentOutOfRangeException(nameof(materials));

            if (CurrentMaterials < materials)
                return false;

            CurrentMaterials -= materials;

            DataChanged?.Invoke();

            return true;
        }
    }
}