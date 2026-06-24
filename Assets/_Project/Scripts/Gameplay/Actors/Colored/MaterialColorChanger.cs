using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class MaterialColorChanger : MonoBehaviour
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private Color _defaultColor = Color.white;

        [SerializeField] private MeshRenderer[] _renderers;
        [SerializeField] private Material _targetMaterial;

        private List<RendererData> _datas;
        private MaterialPropertyBlock _propertyBlock;

        public Color CurrentColor { get; private set; }

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
                data.Renderer.GetPropertyBlock(_propertyBlock, data.MaterialIndex);
                _propertyBlock.SetColor(BaseColorId, color);
                data.Renderer.SetPropertyBlock(_propertyBlock, data.MaterialIndex);
            }
        }

        private void CacheRenderers()
        {
            _datas.Clear();

            foreach (var renderer in _renderers)
            {
                AddRenderer(renderer);

                var childRenderers = renderer.transform.GetComponentsInChildren<Renderer>();

                foreach (var childRenderer in childRenderers)
                {
                    AddRenderer(childRenderer);
                }
            }
        }

        private void AddRenderer(Renderer renderer)
        {
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
