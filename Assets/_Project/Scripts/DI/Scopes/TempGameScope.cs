using BattleBase.Gameplay.CameraNavigation;
using BattleBase.Gameplay.CameraNavigation.InputReader;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using YG;

namespace BattleBase.DI
{
    public class TempGameScope : GameScope
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraArea _cameraArea;
        [SerializeField] private MouseInputConfig _mouseMapCameraConfig;
        [SerializeField] private TouchInputConfig _touchMapCameraConfig;
        [SerializeField] private CameraConfig _cameraConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_camera);
            builder.RegisterComponent<ICameraArea>(_cameraArea);
            builder.RegisterComponent<ICameraConfig>(_cameraConfig);

            builder.Register<IUIPointerChecker, UIPointerChecker>(Lifetime.Scoped);
            builder.Register<ICameraSnapBack, CameraSnapBack>(Lifetime.Scoped);
            builder.Register<ICameraBoundsLimiter, CameraBoundsLimiter>(Lifetime.Scoped);
            builder.Register<IVerticalFactorCalculator, VerticalFactorCalculator>(Lifetime.Scoped);
            builder.Register<ICameraOrientationAdapter, GameSceneCameraOrientationAdapter>(Lifetime.Scoped);
            builder.Register<ICameraTracker, CameraTracker>(Lifetime.Scoped);
            builder.Register<IFrustumProjectionService, FrustumProjectionService>(Lifetime.Scoped);
            builder.Register<ICameraZoom, CameraZoom>(Lifetime.Scoped);
            builder.Register<ICameraDragger, CameraDragger>(Lifetime.Scoped);
            builder.Register<ICameraInputReader, CameraInputReader>(Lifetime.Scoped);
            builder.Register<ICameraAreaService, CameraAreaService>(Lifetime.Scoped);

            if (YG2.envir.isDesktop)
            {
                builder.RegisterComponent(_mouseMapCameraConfig).AsImplementedInterfaces();
                builder.Register<IClickDetector, MouseClickDetector>(Lifetime.Scoped);
                builder.Register<IMouseDragHandler, MouseDragHandler>(Lifetime.Scoped);
                builder.Register<IDragHandler, CompositeMouseDragHandler>(Lifetime.Scoped);
                builder.Register<IKeyboardDragHandler, KeyboardDragHandler>(Lifetime.Scoped);
                builder.Register<IZoomHandler, MouseZoomHandler>(Lifetime.Scoped);
            }
            else
            {
                builder.RegisterComponent(_touchMapCameraConfig).AsImplementedInterfaces();
                builder.Register<IClickDetector, TouchClickDetector>(Lifetime.Scoped);
                builder.Register<IDragHandler, TouchDragHandler>(Lifetime.Scoped);
                builder.Register<IZoomHandler, TouchPinchHandler>(Lifetime.Scoped);
            }
        }
    }
}