using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public interface IScreenshoter
    {
        public ICaptureCamera ScreenshotCamera { get; }

        public Texture2D CaptureObject(
            Vector2Int textureSize,
            DepthBits depth,
            AntiAliasingLevel antiAliasing);
    }
}