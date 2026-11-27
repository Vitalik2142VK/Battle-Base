using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class MaterialColorChanger : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private List<MeshRenderer> _renderers;
        [SerializeField] private Material _targetMaterial;
        [SerializeField] private Color _currentColor = Color.white;
        [SerializeField] private bool _isChangedChilds = true;

        private List<RendererData> _datas;
        private MaterialPropertyBlock _propertyBlock;

#if UNITY_EDITOR
        private Color _oldColor = Color.white;
#endif
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
#if UNITY_EDITOR
            if (_currentColor != _oldColor)
            {
                _oldColor = _currentColor;

                AdoptÑolor();
            }
#endif
        }

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _datas = new();

            CacheRenderers();
            Change(_currentColor);
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

        private void CacheRenderers(List<RendererData> datas = null)
        {
            datas ??= _datas;
            datas.Clear();

            for (int i = _renderers.Count - 1; i >= 0; i--)
            {
                MeshRenderer renderer = _renderers[i];

                if (renderer == null)
                {
                    _renderers.RemoveAt(i);

                    continue;
                }

                AddRenderer(renderer, datas);

                if (_isChangedChilds == false)
                    return;

                var childRenderers = renderer.transform.GetComponentsInChildren<Renderer>(true);

                foreach (var childRenderer in childRenderers)
                    AddRenderer(childRenderer, datas);
            }
        }

        private void AddRenderer(Renderer renderer, List<RendererData> datas)
        {
            if (renderer == null)
                return;

            if (_targetMaterial == null)
                return;

            datas ??= _datas;
            Material[] materials = renderer.sharedMaterials;

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] == _targetMaterial)
                {
                    datas.Add(new RendererData(renderer, i));

                    break;
                }
            }
        }
#if UNITY_EDITOR
        private void AdoptÑolor() 
        {
            List<RendererData> datas = new();
            MaterialPropertyBlock propertyBlock = new();

            CacheRenderers(datas);

            foreach (var data in datas)
            {
                if (data.Renderer == null)
                    continue;

                data.Renderer.GetPropertyBlock(propertyBlock, data.MaterialIndex);
                propertyBlock.SetColor(BaseColorId, _currentColor);
                data.Renderer.SetPropertyBlock(propertyBlock, data.MaterialIndex);
            }
        }
#endif
    }
}
