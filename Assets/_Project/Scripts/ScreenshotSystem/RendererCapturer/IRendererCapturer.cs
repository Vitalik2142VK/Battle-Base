using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public interface IRendererCapturer
    {
        public void RenderToTexture(Camera camera, RenderTexture renderTexture);

        public Texture2D ReadPixels(RenderTexture renderTexture, Vector2Int textureSize);

        public void ClearState(Camera camera);
    }
}