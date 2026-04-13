using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class MapScope : LifetimeScope
    {
        [SerializeField] private MouseMapCameraInputReaderConfig _mouseMapCameraConfig;
        [SerializeField] private TouchMapCameraInputReaderConfig _touchMapCameraConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            if (YG2.envir.isDesktop)
            {
                builder.RegisterComponent<IMouseMapCameraInputReaderConfig>(_mouseMapCameraConfig);
                builder.Register<IMapCameraInputReader, MouseMapCameraInputReader>(Lifetime.Scoped);
            }
            else
            {
                builder.RegisterComponent<ITouchMapCameraInputReaderConfig>(_touchMapCameraConfig);
                builder.Register<IMapCameraInputReader, TouchMapCameraInputReader>(Lifetime.Scoped);
            }
        }
    }
}