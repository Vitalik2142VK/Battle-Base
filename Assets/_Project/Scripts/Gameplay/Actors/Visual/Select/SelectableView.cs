using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Select
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Selectable))]
    public class SelectableView : MonoBehaviour
    {
        private static readonly int ColorPropertyId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private Canvas _ui;
        [SerializeField] private Color _surfaceActiveColor;
        [SerializeField] private Color _edgeSelectedColor;
        [SerializeField] private int _surfaceIndexMaterial;
        [SerializeField] private int _edgeIndexMaterial;

        private Selectable _selectable;
        private MeshRenderer _renderer;
        private MaterialPropertyBlock _propertyBlock;
        private Color _surfaceInitialColor;
        private Color _edgeInitialColor;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _selectable = GetComponent<Selectable>();
            _propertyBlock = new();

            Material surfaceMaterial = _renderer.sharedMaterials[_surfaceIndexMaterial];
            Material edgeMaterial = _renderer.sharedMaterials[_edgeIndexMaterial];
            _surfaceInitialColor = surfaceMaterial != null ? surfaceMaterial.color : Color.white;
            _edgeInitialColor = edgeMaterial != null ? edgeMaterial.color : Color.white;
            HandleInactiveState();
        }

        private void OnEnable()
        {
            _selectable.StateChanged += UpdateState;
            UpdateState();
        }

        private void OnDisable()
        {
            _selectable.StateChanged -= UpdateState;
        }

        private void UpdateState()
        {
            switch (_selectable.State)
            {
                case SelectableState.Inactive:
                    HandleInactiveState();
                    break;

                case SelectableState.Active:
                    HandleActiveState();
                    break;

                case SelectableState.Selected:
                    HandleSelectedState();
                    break;

                default:
                    throw new InvalidOperationException("The specified type is not registered");
            }
        }

        private void HandleInactiveState()
        {
            _ui.gameObject.SetActive(false);
            SetColor(_surfaceIndexMaterial, _surfaceInitialColor);
            SetColor(_edgeIndexMaterial, _edgeInitialColor);
        }

        private void HandleActiveState()
        {
            _ui.gameObject.SetActive(true);
            SetColor(_surfaceIndexMaterial, _surfaceActiveColor);
            SetColor(_edgeIndexMaterial, _edgeInitialColor);
        }

        private void HandleSelectedState()
        {
            _ui.gameObject.SetActive(true);
            SetColor(_surfaceIndexMaterial, _surfaceActiveColor);
            SetColor(_edgeIndexMaterial, _edgeSelectedColor);
        }

        private void SetColor(int materialIndex, Color color)
        {
            _renderer.GetPropertyBlock(_propertyBlock, materialIndex);
            _propertyBlock.SetColor(ColorPropertyId, color);
            _renderer.SetPropertyBlock(_propertyBlock, materialIndex);
        }
    }
}