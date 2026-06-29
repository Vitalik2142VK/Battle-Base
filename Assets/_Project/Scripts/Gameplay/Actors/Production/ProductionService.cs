using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionService : IProductionService
    {
        private readonly List<IProductionOptionsFactory> _factories;

        public ProductionService()
        {
            _factories = new List<IProductionOptionsFactory>();
        }

        public void Enable() { }

        public void Disable() { }

        public void AddProductionFactory(IProductionOptionsFactory factory)
        {
            if (factory == null)
                throw new System.ArgumentNullException(nameof(factory));

            if (_factories.Contains(factory))
                return;

            _factories.Add(factory);
        }

        public IEnumerable<ProductionOption> GetProductionOptions()
        {
            List<ProductionOption> productionOptions = new();

            foreach (var factory in _factories)
                productionOptions.AddRange(factory.Create());

            return productionOptions;
        }
    }
}