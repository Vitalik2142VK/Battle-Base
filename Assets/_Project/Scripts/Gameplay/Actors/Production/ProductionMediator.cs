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
        private ProductionContext _productionContext;

        [Inject]
        public void Construct(
            IClickDetector clickDetector,
            ISelector selector,
            IProductionItemsFactory productionItemFactory,
            IBuildingSitesStorage buildingSitesStorage)
        {
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _productionContext = new ProductionContext(
                productionItemFactory,
                buildingSitesStorage,
                TeamType.Player);
        }

        private void OnEnable()
        {
            _clickDetector.Clicked += OnClickDetected;

            if (_productionContext != null)
                _productionContext.ProductionsChanged += OnSelectViewSpawner;
        }

        private void OnDisable()
        {
            _clickDetector.Clicked -= OnClickDetected;
            _productionContext.ProductionsChanged -= OnSelectViewSpawner;
        }

        private void OnClickDetected(Collider collider)
        {
            if (collider == null)
            {
                HandleUnselect();

                return;
            }

            if (collider.TryGetComponent(out IProductionView productionView))
            {
#if UNITY_EDITOR
                if (DebugSetting.IsAiDisbale) //todo remove on release
                {
                    collider.TryGetComponent(out _selectable);
                    _productionContext.HandleProductionView(productionView);

                    OnSelectViewSpawner();

                    return;
                }
#endif
                if (productionView.TeamType == TeamType.Player)
                {
                    collider.TryGetComponent(out _selectable);
                    _productionContext.HandleProductionView(productionView);

                    OnSelectViewSpawner();

                    return;
                }
            }

            HandleUnselect();
        }

        private void HandleUnselect()
        {
            _productionContext.Clear();
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
            _items.AddRange(_productionContext.GetAvailableItems());

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

        private void OnSelectItem(IProductionData data) => 
            OnSelectViewSpawner();
    }
}