using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class MaterialColorChanger
    {
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
