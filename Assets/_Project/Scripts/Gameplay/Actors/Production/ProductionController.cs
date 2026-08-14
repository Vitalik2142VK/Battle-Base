using BattleBase.Gameplay.Actors.Building;
using BattleBase.UI;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionController
    {
        private readonly IProductionItemsFactory _productionItemFactory;
        private readonly IBuildingSitesStorage _buildingSitesStorage;
        private readonly TeamType _teamType;

        private IRegisteredBuildingSite _selectedBuildingSite;

        public event Action ProductionsChanged;

        public ProductionController(
            IProductionItemsFactory productionItemFactory,
            IBuildingSitesStorage buildingSitesStorage,
            TeamType teamType)
        {
            _productionItemFactory = productionItemFactory ?? throw new ArgumentException(nameof(productionItemFactory));
            _buildingSitesStorage = buildingSitesStorage ?? throw new ArgumentException(nameof(buildingSitesStorage));
            _teamType = teamType;
        }

        public IEnumerable<IProductionItem> GetProductionItems()
        {
            IEnumerable<IProductionOption> productionOptions;

            if (_selectedBuildingSite.TryGetProductionStorage(out IProductionStorage productionStorage))
                productionOptions = productionStorage.GetProductionOptions();
            else
                productionOptions = new List<IProductionOption>();

            return _productionItemFactory.Create(productionOptions);
        }

        public void HandleProductionView(IProductionView productionView)
        {
            if (productionView == null)
                throw new ArgumentNullException(nameof(productionView));

            if (_selectedBuildingSite != null)
                _selectedBuildingSite.StateChanged -= ProductionsChanged;

#if UNITY_EDITOR
            if (DebugSetting.IsAiDisbale) //todo remove on release
            {
                _selectedBuildingSite = _buildingSitesStorage.GetSiteById(productionView.BuildingSiteId);
                _selectedBuildingSite.StateChanged += ProductionsChanged;

                return;
            }
#endif
            _selectedBuildingSite = _buildingSitesStorage.GetSiteById(_teamType, productionView.BuildingSiteId);
            _selectedBuildingSite.StateChanged += ProductionsChanged;
        }

        public void Clear()
        {
            if (_selectedBuildingSite == null)
                return;

            _selectedBuildingSite.StateChanged -= ProductionsChanged;
            _selectedBuildingSite = null;
        }
    }
}