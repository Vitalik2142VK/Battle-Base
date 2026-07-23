using System;
using BattleBase.DI;
using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class Screenshoter : IInjectable, IScreenshoter
    {
        private readonly IScreenshotCaptureCoordinator _screenshotCaptureCoordinator;

        public Screenshoter(IScreenshotCaptureCoordinator screenshotCaptureCoordinator, ICaptureCamera screenshotCamera)
        {
            _screenshotCaptureCoordinator = screenshotCaptureCoordinator ?? throw new ArgumentNullException(nameof(screenshotCaptureCoordinator));
            ScreenshotCamera = screenshotCamera ?? throw new ArgumentNullException(nameof(screenshotCamera));
        }

        public ICaptureCamera ScreenshotCamera { get; private set; }

        public Texture2D CaptureObject(
            Vector2Int textureSize,
            DepthBits depth,
            AntiAliasingLevel antiAliasing)
        {
            ScreenshotCamera.Show();

            Texture2D screenshot = _screenshotCaptureCoordinator.Capture(
                ScreenshotCamera.Camera,
                textureSize,
                depth,
                antiAliasing);

            ScreenshotCamera.Hide();

            return screenshot;
        }
    }
}