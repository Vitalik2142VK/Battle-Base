using BattleBase.Gameplay.Actors.Building;
using BattleBase.UI;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class PlayerProductionContext : IPlayerProductionContext
    {
        private readonly IProductionItemsFactory _productionItemFactory;
        private readonly IBuildingSitesStorage _buildingSitesStorage;

        private IRegisteredBuildingSite _selectedBuildingSite;

        public event Action ProductionsChanged;

        public PlayerProductionContext(
            IProductionItemsFactory productionItemFactory,
            IBuildingSitesStorage buildingSitesStorage)
        {
            _productionItemFactory = productionItemFactory ?? throw new ArgumentException(nameof(productionItemFactory));
            _buildingSitesStorage = buildingSitesStorage ?? throw new ArgumentException(nameof(buildingSitesStorage));
        }

        public IEnumerable<IProductionItem> GetAvailableItems()
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
            {
                _selectedBuildingSite.Unselect();
                _selectedBuildingSite.StateChanged -= OnChangeProduction;
            }

#if UNITY_EDITOR
            if (DebugSettingGameScene.IsAiDisbale) //todo remove on release
            {
                _selectedBuildingSite = _buildingSitesStorage.GetSiteById(productionView.BuildingSiteId);
                _selectedBuildingSite.Select();
                _selectedBuildingSite.StateChanged += OnChangeProduction;

                return;
            }
#endif
            _selectedBuildingSite = _buildingSitesStorage.GetSiteById(TeamType.Player, productionView.BuildingSiteId);
            _selectedBuildingSite.StateChanged += OnChangeProduction;
        }

        public void Clear()
        {
            if (_selectedBuildingSite == null)
                return;

            _selectedBuildingSite.Unselect();
            _selectedBuildingSite.StateChanged -= OnChangeProduction;
            _selectedBuildingSite = null;
        }

        private void OnChangeProduction() =>
            ProductionsChanged?.Invoke();
    }
}