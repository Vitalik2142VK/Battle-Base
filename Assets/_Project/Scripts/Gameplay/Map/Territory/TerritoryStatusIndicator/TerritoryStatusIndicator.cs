using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TerritoryStatusIndicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _base;
        [SerializeField] private Sprite _battle;
        [SerializeField] private float _blackoutFactor = 0.2f;

        private Territory _territory;

        private void Awake() =>
            _territory = GetComponentInParent<Territory>();

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
            Sprite sprite = _territory.Owner switch
            {
                TerritoryOwnerType.Enemy => _base,
                TerritoryOwnerType.Player => _base,
                TerritoryOwnerType.Adjacent => _battle,
                _ => throw new Exception($"Type{nameof(_territory.Owner)} is not registered"),
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
                    Color adjacentColor = Color.Lerp(_territory.Color.Value, Color.black, _blackoutFactor);
                    _renderer.color = adjacentColor;
                }
            }
        }
    }
}