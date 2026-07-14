using BattleBase.Gameplay.CameraNavigation;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class MenuScope : LifetimeScope 
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);
        }
    }
}