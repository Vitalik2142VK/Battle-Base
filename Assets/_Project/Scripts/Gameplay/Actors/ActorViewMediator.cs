using BattleBase.DI;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.Actors.Visual.Select;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.UI;
using BattleBase.UI.PopUps;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors
{
    public class ActorViewMediator : MonoBehaviour, IInjectable
    {
        [SerializeField] private ProductionPanel _productionPanel;

        private List<IProductionItem> _items = new();

        private IActorViewSpawner _currentViewSpawner;
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

            if (collider.TryGetComponent(out _currentViewSpawner))
            {
                if (_currentViewSpawner.TeamType == TeamType.Player)
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
            _items = _productionItemFactory.Create(_currentViewSpawner.ProductionOptions);

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

        private void OnSelectItem(ProductionOption productionOption)
        {
            productionOption.Execute();
            IProductionData info = productionOption.ProductionData;

            if (_selectable != null && info.IsSummable == false)
            {
                HandleUnselectEntity();
                _selectable = null;
            }
        }
    }
}