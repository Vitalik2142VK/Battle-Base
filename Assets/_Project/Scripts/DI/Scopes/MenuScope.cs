using BattleBase.Gameplay.CameraNavigation;
using BattleBase.ScreenshotSystem;
using BattleBase.ShopSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class MenuScope : LifetimeScope 
    {
        [SerializeField] private Screenshoter _screenshoter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);

            builder.RegisterComponent(_screenshoter);
            builder.Register<PreviewCreator>(Lifetime.Singleton);
        }
    }
}