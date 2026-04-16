using BattleBase.Gameplay.Map;
using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class MapScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraFrustumProjector _cameraFrustumProjector;
        [SerializeField] private MouseCameraInputReaderConfig _mouseMapCameraConfig;
        [SerializeField] private TouchCameraInputReaderConfig _touchMapCameraConfig;
        [SerializeField] private CameraConfig _cameraConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_camera);
            builder.RegisterComponent<ICameraFrustumProjector>(_cameraFrustumProjector);
            builder.RegisterComponent<ICameraConfig>(_cameraConfig);
            builder.Register<IUIPointerChecker, UIPointerChecker>(Lifetime.Scoped);
            builder.Register<ICameraSnapBack, CameraSnapBack>(Lifetime.Scoped);
            builder.Register<ICameraBoundsLimiter, CameraBoundsLimiter>(Lifetime.Scoped);
            builder.Register<ICameraAngleCompensator, CameraAngleCompensator>(Lifetime.Scoped);
            builder.Register<ICameraZoom, CameraZoom>(Lifetime.Scoped);
            builder.Register<ICameraDragger, CameraDragger>(Lifetime.Scoped);

            if (YG2.envir.isDesktop)
            {
                builder.RegisterComponent(_mouseMapCameraConfig).AsImplementedInterfaces();
                builder.Register<MouseDragHandler>(Lifetime.Scoped);
                builder.Register<KeyboardDragHandler>(Lifetime.Scoped);
                builder.Register<MouseZoomHandler>(Lifetime.Scoped);
                builder.Register<ICameraInputReader, MouseCameraInputReader>(Lifetime.Scoped);
            }
            else
            {
                builder.RegisterComponent(_touchMapCameraConfig).AsImplementedInterfaces();
                builder.Register<TouchDragHandler>(Lifetime.Scoped);
                builder.Register<TouchPinchHandler>(Lifetime.Scoped);
                builder.Register<ICameraInputReader, TouchCameraInputReader>(Lifetime.Scoped);
            }
        }
    }
}