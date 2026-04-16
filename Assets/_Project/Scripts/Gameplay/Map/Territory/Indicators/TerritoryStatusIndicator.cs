using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryStatusIndicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _base;
        [SerializeField] private Sprite _battle;
        [SerializeField] private float _ownerColorBlackoutFactor = 0.2f;

        private Territory _territory;

        private void Awake()
        {
            _territory = GetComponentInParent<Territory>();

            if (_territory == null)
                Debug.LogError($"{nameof(TerritoryStatusIndicator)} on {gameObject.name} requires a {nameof(Territory)} component in parent.", this);
        }

        private void OnEnable()
        {
            _territory.OwnerChanged += OnOwnerChanged;
            OnOwnerChanged();

            _territory.ColorChanged += OnColorChanged;
            OnColorChanged();
        }

        private void OnDisable()
        {
            _territory.OwnerChanged -= OnOwnerChanged;
            _territory.ColorChanged -= OnColorChanged;
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
                TerritoryOwnerType.Adjacent => _battle,
                _ => throw new ArgumentOutOfRangeException(nameof(_territory.Owner), _territory.Owner, $"Type is not registered"),
            };

            _renderer.sprite = sprite;

            OnColorChanged();
        }

        private void OnColorChanged()
        {
            if (_territory.Color.HasValue)
            {
                if (_renderer.sprite == _battle)
                {
                    _renderer.color = Color.white;
                }
                else
                {
                    Color adjacentColor = Color.Lerp(_territory.Color.Value, Color.black, _ownerColorBlackoutFactor);
                    _renderer.color = adjacentColor;
                }
            }
        }
    }
}