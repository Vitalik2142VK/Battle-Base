using BattleBase.DI;
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

        private IProductionView _productionView;
        private IClickDetector _clickDetector;
        private ISelector _selector;
        private IProductionItemFactory _productionItemFactory;

        private ISelectable _selectable;

        [Inject]
        public void Construct(
            IClickDetector clickDetector,
            ISelector selector,
            IProductionItemFactory productionItemFactory)
        {
            _clickDetector = clickDetector ?? throw new ArgumentNullException(nameof(clickDetector));
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _productionItemFactory = productionItemFactory ?? throw new ArgumentNullException(nameof(productionItemFactory));
        }

        private void OnEnable() =>
            _clickDetector.Clicked += OnClickDetected;

        private void OnDisable() =>
            _clickDetector.Clicked -= OnClickDetected;

        private void OnClickDetected(Collider collider)
        {
            if (collider == null)
            {
                HandleUnselectEntity();

                return;
            }

            if (collider.TryGetComponent(out _productionView))
            {
#if UNITY_EDITOR
                if (DebugSetting.IsAiDisbale) //todo remove on release
                {
                    collider.TryGetComponent(out _selectable);

                    SelectViewSpawner();

                    return;
                }
#endif
                if (_productionView.TeamType == TeamType.Player)
                {
                    collider.TryGetComponent(out _selectable);

                    SelectViewSpawner();

                    return;
                }
            }

            HandleUnselectEntity();
        }

        private void SelectViewSpawner()
        {
            if (_selector.TrySelect(_selectable) == false)
                _selector.Unselect();

            _productionPanel.ClearContext();
            _items = _productionItemFactory.Create(_productionView.ProductionOptions);

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

        private void HandleUnselectEntity()
        {
            _selector.Unselect();
            _productionPanel.Hide();
            _items.Clear();
        }

        private void OnSelectItem(IProductionData data)
        {
            if (_selectable != null && data.IsSummable == false)
            {
                HandleUnselectEntity();
                _selectable = null;
            }
        }
    }
}