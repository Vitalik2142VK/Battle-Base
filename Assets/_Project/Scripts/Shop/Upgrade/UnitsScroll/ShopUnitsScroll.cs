using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Commands;
using BattleBase.Utils.Extensions;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopUnitsScroll : MonoBehaviour
    {
        [SerializeField] private CommandRebuildLayout _commandRebuildLayout;
        [SerializeField] private Transform _content;
        [SerializeField] private ShopUnitItemView _prefab;

        private readonly List<ShopUnitItemView> _items = new();

        private ActorsUpgradeModel _unitsUpgradeModel;
        private bool _isInitialized;
        private bool _isSyncingSelection;

        public ShopUnitItemView CurrentItem { get; private set; }

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));

            _unitsUpgradeModel.UnitSelectionChanged += OnModelSelectionChanged;
        }

        private void OnEnable() =>
            SyncSelectionFromModel();

        private void OnDestroy()
        {
            if (_unitsUpgradeModel != null)
                _unitsUpgradeModel.UnitSelectionChanged -= OnModelSelectionChanged;
        }

        public void Init(IReadOnlyList<IActorItemConfig> infos, List<Sprite> previews)
        {
            _content.ClearChilds();
            _items.Clear();
            CurrentItem = null;

            for (int i = 0; i < infos.Count; i++)
            {
                IActorItemConfig info = infos[i];
                ShopUnitItemView item = Instantiate(_prefab, _content);
                item.SetInfo(info, previews[i], Select);
                item.Unselect();
                _items.Add(item);
            }

            _isInitialized = true;
            SyncSelectionFromModel();
        }

        public void Select(ShopUnitItemView item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _unitsUpgradeModel.SelectActor(item.Info);
        }

        private void OnModelSelectionChanged() =>
            SyncSelectionFromModel();

        private void SyncSelectionFromModel()
        {
            if (_isSyncingSelection)
                return;

            if (_isInitialized == false)
                return;

            if (_items.Count == 0)
                return;

            if (_unitsUpgradeModel == null)
                return;

            _isSyncingSelection = true;

            try
            {
                IActorItemConfig selected = _unitsUpgradeModel.Selected;
                ShopUnitItemView targetItem = selected != null ? FindItemById(selected.Id) : null;

                if (targetItem == null)
                {
                    targetItem = _items.First();
                    _unitsUpgradeModel.SelectActor(targetItem.Info);
                }

                ApplyVisualSelection(targetItem);
            }
            finally
            {
                _isSyncingSelection = false;
            }
        }

        private void ApplyVisualSelection(ShopUnitItemView item)
        {
            UnselectAll();
            item.Select();
            CurrentItem = item;

            if (gameObject.activeInHierarchy)
                _commandRebuildLayout.Execute();
        }

        private ShopUnitItemView FindItemById(string id)
        {
            foreach (ShopUnitItemView item in _items)
            {
                if (item.Info.Id == id)
                    return item;
            }

            return null;
        }

        private void UnselectAll()
        {
            foreach (ShopUnitItemView item in _items)
                item.Unselect();
        }
    }
}