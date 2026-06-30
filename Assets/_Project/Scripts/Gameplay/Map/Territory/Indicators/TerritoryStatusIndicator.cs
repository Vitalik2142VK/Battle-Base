using System;
using BattleBase.DI;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryStatusIndicator : MonoBehaviour, IInjectable
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _base;
        [SerializeField] private Sprite _battle;
        [SerializeField] private float _ownerColorBlackoutFactor = 0.2f;

        private Territory _territory;
        private TeamColorModel _colorModel;

        [Inject]
        public void Construct(TeamColorModel colorModel) =>
            _colorModel = colorModel ?? throw new ArgumentNullException(nameof(colorModel));

        private void OnEnable()
        {
            _colorModel.Changed += OnColorChanged;
            Subscribe();
        }

        private void OnDisable()
        {
            _colorModel.Changed -= OnColorChanged;
            Unsubscribe();
        }

        public void SetTerritory(Territory territory)
        {
            _territory = territory != null ? territory : throw new ArgumentNullException(nameof(territory));

            if (_territory == territory)
                return;

            Unsubscribe();

            Transform territoryTransform = territory.transform;
            transform.SetParent(territoryTransform);
            transform.position = territoryTransform.position;

            Subscribe();
        }

        private void Subscribe()
        {
            if (_territory == null)
                return;

            Unsubscribe();

            _territory.OwnerChanged += OnOwnerChanged;
            OnOwnerChanged();
        }

        private void Unsubscribe()
        {
            if (_territory == null)
                return;

            _territory.OwnerChanged -= OnOwnerChanged;
        }

        private void OnOwnerChanged()
        {
            if (_base == null || _battle == null)
            {
                Debug.LogError($"{nameof(TerritoryStatusIndicator)}: {nameof(_base)} or {nameof(_battle)} sprite is not assigned.", this);

                return;
            }

            Sprite sprite = _territory.Owner switch
            {
                TerritoryOwnerType.Enemy => _base,
                TerritoryOwnerType.Player => _base,
                TerritoryOwnerType.Contested => _battle,
                _ => throw new ArgumentOutOfRangeException(nameof(_territory.Owner), _territory.Owner, $"Type is not registered"),
            };

            _renderer.sprite = sprite;

            OnColorChanged();
        }

        private void OnColorChanged()
        {
            TerritoryOwnerType owner = _territory.Owner;

            _renderer.color = owner switch
            {
                TerritoryOwnerType.Enemy => ModifyColor(_colorModel.EnemyColor),
                TerritoryOwnerType.Player => ModifyColor(_colorModel.PlayerColor),
                TerritoryOwnerType.Contested => Color.white,
                _ => throw new ArgumentOutOfRangeException(nameof(owner), owner, $"Type is not registered"),
            };
        }

        private Color ModifyColor(Color color) =>
            Color.Lerp(color, Color.black, _ownerColorBlackoutFactor);
    }
}