using BattleBase.DI;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Spawn;
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
        private IBuildingSiteSelector _selector;
        private IProductionItemFactory _productionItemFactory;

        private IBuildingSite _currentBuildingSite;

        [Inject]
        public void Construct(
            IClickDetector clickDetector,
            IBuildingSiteSelector selector,
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

            if (collider.TryGetComponent(out IActorViewSpawner viewSpawner))
            {
                collider.TryGetComponent(out _currentBuildingSite);
                _currentViewSpawner = viewSpawner;

                SelectViewSpawner();

                return;
            }

            HandleUnselectEntity();
        }

        private void SelectViewSpawner()
        {
            IBuildingSite buildingSite = _currentViewSpawner.BuildingSite;
            _selector.TrySelect(buildingSite);

            _productionPanel.ClearContext();
            _items = _productionItemFactory.Create(_currentViewSpawner.ActorsData);

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

        private void OnSelectItem(IProductionItem item)
        {
            _currentViewSpawner.SelectActorData(item.Info);

            if (_currentBuildingSite != null)
            {
                HandleUnselectEntity();
                _currentBuildingSite.Hide();
                _currentBuildingSite = null;
            }
        }
    }
}