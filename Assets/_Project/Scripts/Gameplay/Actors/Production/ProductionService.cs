using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionService : IProductionService
    {
        private readonly List<IProductionStorage> _productionStorages;
        private readonly List<ProductionOption> _productionOptions;

        public ProductionService()
        {
            _productionStorages = new List<IProductionStorage>();
            _productionOptions = new List<ProductionOption>();
        }

        public IEnumerable<ProductionOption> ProductionOptions => _productionOptions;

        public void Enable()
        {
            foreach (var productionStorage in _productionStorages)
                _productionOptions.AddRange(productionStorage.ProductionOptions);
        }

        public void Disable()
        {
            _productionOptions.Clear();
        }

        public void AddProductionStorage(IProductionStorage productionStorage)
        {
            if (productionStorage == null)
                throw new System.ArgumentNullException(nameof(productionStorage));

            if (_productionStorages.Contains(productionStorage))
                return;

            _productionStorages.Add(productionStorage);
        }
    }
}