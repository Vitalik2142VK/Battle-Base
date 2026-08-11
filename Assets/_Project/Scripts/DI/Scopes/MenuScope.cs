using BattleBase.Gameplay.CameraNavigation;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class MenuScope : LifetimeScope
    {
        private IContainerBuilder _builder;

        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;

            BuildScreenTrackers();
        }

        private void BuildScreenTrackers()
        {
            _builder.Register<IScreenSizeTracker, ScreenSizeTracker>(Lifetime.Singleton);
            _builder.Register<IScreenOrientationTracker, ScreenOrientationTracker>(Lifetime.Singleton);
        }
    }
}