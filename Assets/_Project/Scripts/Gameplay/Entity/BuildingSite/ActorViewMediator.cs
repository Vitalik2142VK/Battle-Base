using BattleBase.Commands;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using BattleBase.UI;
using BattleBase.UI.PopUps;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay
{
    public class ActorViewMediator : MonoBehaviour, IInjectable
    {
        [SerializeField] private ProductionPanel _productionPanel;
        [SerializeField] private CommandShowHidePopUps _commandShowProductionPanel;
        [SerializeField] private CommandShowHidePopUps _commandHideProductionPanel;

        private List<IProductionItem> _items = new();

        private IActorViewSpawner _currentViewSpawner;
        private IClickDetector _clickDetector;
        private IBuildingSiteSelector _selector;
        private IProductionItemFactory _productionItemFactory;

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
                return;

            if (collider.TryGetComponent(out IActorViewSpawner viewSpawner))
            {
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
                _commandHideProductionPanel.Execute();
            else
                _commandShowProductionPanel.Execute();

            foreach (IProductionItem item in _items)
            {
                _productionPanel.AddItem(item);
                item.ItemClicked += OnSelectItem;
            }
        }

        private void HandleUnselectEntity()
        {
            _selector.Unselect();
            _commandHideProductionPanel.Execute();
            _items.Clear();
        }

        private void OnSelectItem(IProductionItem item)
        {
            _currentViewSpawner.SelectActorData(item.Info);
        }
    }
}