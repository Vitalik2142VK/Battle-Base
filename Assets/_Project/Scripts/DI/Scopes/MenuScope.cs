using BattleBase.Gameplay.CameraNavigation;
using BattleBase.PreviewCreatingSystem;
using BattleBase.ScreenshotSystem;
using BattleBase.ShopSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class MenuScope : LifetimeScope
    {
        [SerializeField] private CaptrureCamera _captureCameraPrefab;

        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            BuildScreenTrackers();
            BuildScreenshotSystem();
        }

        private void BuildScreenTrackers()
        {
            _builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            _builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);
        }

        private void BuildScreenshotSystem()
        {
            _builder.Register<IPreviewCreator, PreviewCreator>(Lifetime.Singleton);
            _builder.Register<IScreenshoter, Screenshoter>(Lifetime.Singleton);
            _builder.Register<IScreenshotCaptureCoordinator, ScreenshotCaptureCoordinator>(Lifetime.Singleton);
            _builder.Register<IRendererCapturer, RendererCapturer>(Lifetime.Singleton);
            _builder.Register<IRenderTextureFactory, RenderTextureFactory>(Lifetime.Singleton);
            _builder.Register<IModelCenterCalculator, ModelCenterCalculator>(Lifetime.Singleton);
            _builder.RegisterComponentInNewPrefab(_captureCameraPrefab, Lifetime.Singleton).As<ICaptureCamera>();
        }
    }
}