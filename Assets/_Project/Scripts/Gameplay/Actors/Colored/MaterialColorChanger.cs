using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class MaterialColorChanger : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private Color _defaultColor = Color.white;

        [SerializeField] private List<MeshRenderer> _renderers;
        [SerializeField] private Material _targetMaterial;

        private List<RendererData> _datas;
        private MaterialPropertyBlock _propertyBlock;

        public Color CurrentColor { get; private set; }

        public void OnValidate()
        {
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
            Change(_defaultColor);
        }

        public void Change(Color color)
        {
            CurrentColor = color;

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
