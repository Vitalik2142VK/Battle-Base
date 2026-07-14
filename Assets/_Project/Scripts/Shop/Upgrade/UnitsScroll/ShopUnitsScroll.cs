using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Commands;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Map;
using BattleBase.ScreenshotSystem;
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
        [SerializeField] private Screenshoter _screenshoter;

        private readonly List<ShopUnitItemView> _items = new();

        private ActorsUpgradeModel _unitsUpgradeModel;
        private TeamColorModel _teamColorModel;

        public ShopUnitItemView CurrentItem { get; private set; }

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel, TeamColorModel teamColorModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _teamColorModel = teamColorModel ?? throw new ArgumentNullException(nameof(teamColorModel));

            Init(unitsUpgradeModel.Infos);
        }

        public void Init(IReadOnlyList<IShopActorItemConfig> infos)
        {
            _content.ClearChilds();
            _items.Clear();

            Vector2 centerPivot = new(0.5f, 0.5f);
            int squareTextureSize = 256;
            Rect rect = new(0, 0, squareTextureSize, squareTextureSize);

            foreach (IShopActorItemConfig info in infos)
            {
                GameObject actor = Instantiate(info.CleanPrefab);
                actor.transform.localScale = actor.transform.localScale * info.PreviewScreenScale;

                if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                    colorChanger.Change(_teamColorModel.PlayerColor);

                Texture2D texture = _screenshoter.CaptureObject(actor, squareTextureSize, squareTextureSize);

                Sprite preview = Sprite.Create(
                    texture,
                    rect,
                    centerPivot);

                actor.SetActive(false);
                Destroy(actor);

                ShopUnitItemView item = Instantiate(_prefab, _content);
                item.SetInfo(info, preview, Select);
                item.Unselect();
                _items.Add(item);
            }

            Select(_items.First());
        }

        public void Select(ShopUnitItemView item)
        {
            UnselectAll();
            item.Select();
            CurrentItem = item;

            _unitsUpgradeModel.SelectUnit(item.Info);

            _commandRebuildLayout.Execute();
        }

        private void UnselectAll()
        {
            foreach (ShopUnitItemView item in _items)
                item.Unselect();
        }
    }
}