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

        public ShopUnitItemView CurrentItem { get; private set; }

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
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

            if (_items.Count > 0)
                Select(_items.First());
        }

        public void Select(ShopUnitItemView item)
        {
            UnselectAll();
            item.Select();
            CurrentItem = item;

            _unitsUpgradeModel.SelectActor(item.Info);
            _commandRebuildLayout.Execute();
        }

        private void UnselectAll()
        {
            foreach (ShopUnitItemView item in _items)
                item.Unselect();
        }
    }
}