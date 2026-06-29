using System;
using BattleBase.DI;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Territory))]
    public class TerritoryColorIndicator : MonoBehaviour, IInjectable
    {
        private const string ColorProperty = "_BaseColor";
        private static readonly int ColorPropertyID = Shader.PropertyToID(ColorProperty);

        [SerializeField] private int _surfaceMaterialIndex = 1;

        private MeshRenderer _meshRenderer;
        private Territory _territory;
        private MaterialPropertyBlock _materialPropertyBlock;
        private TeamColorModel _colorModel;

        [Inject]
        public void Construct(TeamColorModel colorModel) =>
            _colorModel = colorModel ?? throw new ArgumentNullException(nameof(colorModel));

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _territory = GetComponent<Territory>();

            _materialPropertyBlock = new();
        }

        private void OnEnable()
        {
            _territory.OwnerChanged += OnOwnerChanged;
            _colorModel.Changed += OnColorChanged;
            UpdateColor();
        }

        private void OnDisable()
        {
            _territory.OwnerChanged -= OnOwnerChanged;
            _colorModel.Changed -= OnColorChanged;
        }

        private void UpdateColor()
        {
            TerritoryOwnerType owner = _territory.Owner;

            Color color = owner switch
            {
                TerritoryOwnerType.Enemy => _colorModel.EnemyColor,
                TerritoryOwnerType.Player => _colorModel.PlayerColor,
                TerritoryOwnerType.Contested => Color.Lerp(_colorModel.EnemyColor, Color.white, TeamColorModel.LightenFactor),
                _ => throw new ArgumentOutOfRangeException(nameof(owner), owner, "Type is not registered"),
            };

            _meshRenderer.GetPropertyBlock(_materialPropertyBlock, _surfaceMaterialIndex);
            _materialPropertyBlock.SetColor(ColorPropertyID, color);
            _meshRenderer.SetPropertyBlock(_materialPropertyBlock, _surfaceMaterialIndex);
        }

        private void OnOwnerChanged() =>
            UpdateColor();

        private void OnColorChanged() =>
            UpdateColor();
    }
}