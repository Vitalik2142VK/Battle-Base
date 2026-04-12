using BattleBase.InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class MapScope : LifetimeScope
    {
        [SerializeField] private MouseMapCameraInputReaderConfig _mouseMapCameraConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent<IMouseMapCameraInputReaderConfig>(_mouseMapCameraConfig);
            builder.Register<IMapCameraInputReader, MouseMapCameraInputReader>(Lifetime.Scoped);
        }
    }
}