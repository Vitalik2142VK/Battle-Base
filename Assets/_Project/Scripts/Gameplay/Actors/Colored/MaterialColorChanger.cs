using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class MaterialColorChanger : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private List<MeshRenderer> _renderers;
        [SerializeField] private Material _targetMaterial;

        private List<RendererData> _datas;
        private MaterialPropertyBlock _propertyBlock;
        private Color _currentColor;

        public Color CurrentColor => _currentColor;

        public void OnValidate()
        {
            if (_renderers == null)
                return;

            for (int i = 0; i < _renderers.Count; i++)
            {
                if (_renderers[i] == null)
                    _renderers.RemoveAt(i--);
            }
        }

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _datas = new();

            CacheRenderers();
        }

        public void Change(Color color)
        {
            _currentColor = color;

            foreach (var data in _datas)
            {
                if (data.Renderer == null)
                    continue;

                data.Renderer.GetPropertyBlock(_propertyBlock, data.MaterialIndex);
                _propertyBlock.SetColor(BaseColorId, color);
                data.Renderer.SetPropertyBlock(_propertyBlock, data.MaterialIndex);
            }
        }

        private void CacheRenderers()
        {
            _datas.Clear();

            for (int i = _renderers.Count - 1; i >= 0; i--)
            {
                MeshRenderer renderer = _renderers[i];

                if (renderer == null)
                {
                    _renderers.RemoveAt(i);

                    continue;
                }

                AddRenderer(renderer);
                var childRenderers = renderer.transform.GetComponentsInChildren<Renderer>(true);

                foreach (var childRenderer in childRenderers)
                    AddRenderer(childRenderer);
            }
        }

        private void AddRenderer(Renderer renderer)
        {
            if (renderer == null)
                return;

            if (_targetMaterial == null)
                return;

            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == _targetMaterial)
                {
                    _datas.Add(new RendererData(renderer, i));

                    break;
                }
            }
        }
    }
}
