using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class ScreenshotCaptureCoordinator : IScreenshotCaptureCoordinator
    {
        private readonly IRendererCapturer _capturer;
        private readonly IRenderTextureFactory _textureFactory;

        public ScreenshotCaptureCoordinator(IRendererCapturer capturer, IRenderTextureFactory textureFactory)
        {
            _capturer = capturer;
            _textureFactory = textureFactory;
        }

        public Texture2D Capture(
            Camera camera, 
            Vector2Int textureSize, 
            DepthBits depth, 
            AntiAliasingLevel antiAliasing)
        {
            RenderTexture renderTexture = _textureFactory.GetOrCreate(textureSize, depth, antiAliasing);
            _capturer.RenderToTexture(camera, renderTexture);

            Texture2D result = _capturer.ReadPixels(renderTexture, textureSize);
            _capturer.ClearState(camera);

            return result;
        }
    }
}