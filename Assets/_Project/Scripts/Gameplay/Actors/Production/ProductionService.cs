using System;
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

        public Type KeyType => typeof(IProductionService);

        public bool HasProductions => FindProductionOptions().Count > 0;

        public int BuildingSiteId { get; private set; }

        public void Enable()
        {
            BuildingSiteId = -1;
        }

        public void AddProductionFactory(IProductionOptionsFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (_factories.Contains(factory))
                return;

            _factories.Add(factory);
        }

        public void SetBuildingSiteId(int id)
        {
            if (id < 0) 
                throw new ArgumentOutOfRangeException(nameof(id));

            BuildingSiteId = id;
        }

        public IEnumerable<IProductionOption> GetProductionOptions() =>
            FindProductionOptions();

        public void Disable() { }

        private List<IProductionOption> FindProductionOptions()
        {
            List<IProductionOption> productionOptions = new();

            foreach (var factory in _factories)
                productionOptions.AddRange(factory.Create());

            return productionOptions;
        }
    }
}