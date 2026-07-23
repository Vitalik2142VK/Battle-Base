using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public interface IScreenshotCaptureCoordinator
    {
        public Texture2D Capture(
            Camera camera,
            Vector2Int textureSize,
            DepthBits depth,
            AntiAliasingLevel antiAliasing);
    }
}