using BattleBase.DI;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Visual.Select;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.UI;
using BattleBase.UI.PopUps;
using BattleBase.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionMediator : MonoBehaviour, IInjectable
    {
        [SerializeField] private ProductionPanel _productionPanel;

        private List<IProductionItem> _items = new();

        private IClickDetector _clickDetector;
        private ISelector _selector;
        private ISelectable _selectable;
        private ProductionController _productionController;

        [Inject]
        public void Construct(
            IClickDetector clickDetector,
            ISelector selector,
            IProductionItemsFactory productionItemFactory,
            IBuildingSitesStorage buildingSitesStorage)
        {
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _productionController = new ProductionController(
                productionItemFactory,
                buildingSitesStorage,
                TeamType.Player);
        }

        private void OnEnable()
        {
            _clickDetector.Clicked += OnClickDetected;

            if (_productionController != null)
                _productionController.ProductionsChanged += OnSelectViewSpawner;
        }

        private void OnDisable()
        {
            _clickDetector.Clicked -= OnClickDetected;
            _productionController.ProductionsChanged -= OnSelectViewSpawner;
        }

        private void OnClickDetected(Collider collider)
        {
            if (collider == null)
            {
                HandleUnselectEntity();

                return;
            }

            if (collider.TryGetComponent(out IProductionView productionView))
            {
#if UNITY_EDITOR
                if (DebugSetting.IsAiDisbale) //todo remove on release
                {
                    collider.TryGetComponent(out _selectable);
                    _productionController.HandleProductionView(productionView);

                    OnSelectViewSpawner();

                    return;
                }
#endif
                if (productionView.TeamType == TeamType.Player)
                {
                    collider.TryGetComponent(out _selectable);
                    _productionController.HandleProductionView(productionView);

                    OnSelectViewSpawner();

                    return;
                }
            }

            HandleUnselectEntity();

            _productionController.Clear();
        }

        private void HandleUnselectEntity()
        {
            _selector.Unselect();
            _productionPanel.Hide();
            _items.Clear();
        }

        private void OnSelectViewSpawner()
        {
            if (_selector.TrySelect(_selectable) == false)
                _selector.Unselect();

            _productionPanel.ClearContext();
            _items.Clear();
            _items.AddRange(_productionController.GetProductionItems());

            if (_items.Count == 0)
                _productionPanel.Hide();
            else
                _productionPanel.Show();

            foreach (IProductionItem item in _items)
            {
                _productionPanel.AddItem(item);
                item.ItemClicked += OnSelectItem;
            }
        }

        private void OnSelectItem(IProductionData data)
        {
            if (_selectable != null && data.IsHidden == false)
            {
                HandleUnselectEntity();
                _selectable = null;
                _productionController.Clear();
            }
        }
    }
}