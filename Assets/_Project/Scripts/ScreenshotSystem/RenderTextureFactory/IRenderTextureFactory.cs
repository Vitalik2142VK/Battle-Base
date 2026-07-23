using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public interface IRenderTextureFactory
    {
        public RenderTexture GetOrCreate(
            Vector2Int textureSize, 
            DepthBits depth, 
            AntiAliasingLevel antiAliasing);
    }
}