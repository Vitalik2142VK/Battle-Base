using BattleBase.Gameplay.MiniMap;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    [RequireComponent(typeof(Trackable))]
    public class ColoredActorView : MonoBehaviour, IColoredActorView
    {
        private static readonly int BaseColorId = Shader.PropertyToID("_BaseColor");

        [SerializeField] private MeshRenderer[] _renderers;
        [SerializeField] private Material _targetMaterial;

        private IColored _colored;
        private List<RendererData> _datas;
        private MaterialPropertyBlock _propertyBlock;
        private Trackable _trackable;

        private void Awake()
        {
            _trackable = GetComponent<Trackable>();
            _propertyBlock = new MaterialPropertyBlock();
            _datas = new();

            CacheRenderers();
        }

        private void OnEnable()
        {
            if (_colored != null)
                CnangeColor();
        }

        public void Init(IColored colored)
        {
            _colored ??= colored ?? throw new System.ArgumentNullException(nameof(colored));

            CnangeColor();
        }

        private void CnangeColor()
        {
            foreach (var data in _datas)
            {
                data.Renderer.GetPropertyBlock(_propertyBlock, data.MaterialIndex);
                _propertyBlock.SetColor(BaseColorId, _colored.Color);
                data.Renderer.SetPropertyBlock(_propertyBlock, data.MaterialIndex);
            }

            _trackable.SetColor(_colored.Color);
        }

        private void CacheRenderers()
        {
            _datas.Clear();

            foreach (var renderer in _renderers)
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

        private readonly struct RendererData
        {
            public readonly Renderer Renderer;
            public readonly int MaterialIndex;

            public RendererData(Renderer renderer, int materialIndex)
            {
                Renderer = renderer;
                MaterialIndex = materialIndex;
            }
        }
    }
}
