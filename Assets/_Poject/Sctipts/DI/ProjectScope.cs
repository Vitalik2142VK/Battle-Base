using BattleBase.Services.InputReader;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class ProjectScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputReader>(Lifetime.Singleton)
                .As<IInputReader>();
        }
    }
}