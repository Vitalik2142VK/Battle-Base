using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Territory))]
    public class TerritoryColorIndicator : MonoBehaviour
    {
        private static readonly int ColorPropertyID = Shader.PropertyToID("_BaseColor");

        [SerializeField] private int _materialIndex = 1;

        private MeshRenderer _meshRenderer;
        private Territory _territory;
        private MaterialPropertyBlock _propertyBlock;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _territory = GetComponent<Territory>();
            _propertyBlock = new();
        }

        private void OnEnable()
        {
            _territory.ColorChanged += OnTerrritoryChanged;
            OnTerrritoryChanged();
        }

        private void OnDisable() =>
            _territory.ColorChanged -= OnTerrritoryChanged;

        private void OnTerrritoryChanged()
        {
            ApplyColor(_territory.Color);
        }

        private void ApplyColor(Color? color)
        {
            if (color.HasValue == false)
                return;

            _meshRenderer.GetPropertyBlock(_propertyBlock, _materialIndex);
            _propertyBlock.SetColor(ColorPropertyID, color.Value);
            _meshRenderer.SetPropertyBlock(_propertyBlock, _materialIndex);
        }
    }
}