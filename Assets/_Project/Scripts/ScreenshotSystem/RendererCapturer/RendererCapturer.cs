using System;
using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class RendererCapturer : IRendererCapturer
    {
        public void RenderToTexture(Camera camera, RenderTexture renderTexture)
        {
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            camera.targetTexture = renderTexture;
            camera.Render();
        }

        public Texture2D ReadPixels(RenderTexture renderTexture, Vector2Int textureSize)
        {
            if (renderTexture == null)
                throw new ArgumentNullException(nameof(renderTexture));

            RenderTexture.active = renderTexture;
            Texture2D screenshot = new(textureSize.x, textureSize.y, TextureFormat.RGBA32, false);
            screenshot.ReadPixels(new Rect(0, 0, textureSize.x, textureSize.y), 0, 0);
            screenshot.Apply();

            return screenshot;
        }

        public void ClearState(Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException(nameof(camera));

            RenderTexture.active = null;
            camera.targetTexture = null;
        }
    }
}