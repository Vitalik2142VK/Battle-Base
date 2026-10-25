using System.Collections.Generic;
using BattleBase.AdvService;
using BattleBase.AudioService;
using BattleBase.AuthService;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Availability;
using BattleBase.Gameplay.Map;
using BattleBase.PauseService;
using BattleBase.PreviewCreatingSystem;
using BattleBase.PurchaseService;
using BattleBase.SaveService;
using BattleBase.SceneLoadingService;
using BattleBase.ScreenshotSystem;
using BattleBase.ShopSystem;
using BattleBase.UpdateService;
using UnityEngine;
using UnityEngine.Audio;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private Music _music;
        [SerializeField] private Sfx _sfx;
        [SerializeField] private Updater _updater;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private ActorsUpgradeConfig _actorsUpgradeConfig;
        [SerializeField] private TeamColorSetConfig _teamColorSetConfig;
        [SerializeField] private List<TerritoryConfig> _territoryConfigs;
        [SerializeField] private CaptureCamera _captureCamera;
        [SerializeField] private AllActorConfigs _allActorConfigs;
        [SerializeField] private PurchasesCatalog _purchasesCatalog;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPauseSwitcher, PauseSwitcher>(Lifetime.Singleton);
            builder.Register<YandexGameSaveSystemAdapter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IAdvertisingService, YandexGameAdvertisingAdapter>(Lifetime.Singleton);
            builder.Register<IAuthorizationService, YandexGameAuthAdapter>(Lifetime.Singleton);

            builder.RegisterInstance(_purchasesCatalog).As<IPurchaseCatalog>();
            builder.Register<IPurchaseService, YandexGamePurchaseAdapter>(Lifetime.Singleton);

            builder.Register<IPurchaseHandler, NoAdsPurchaseHandler>(Lifetime.Singleton);
            builder.Register<IPurchaseHandler, CoinsPurchaseHandler>(Lifetime.Singleton);

            builder.Register<PurchaseMediator>(Lifetime.Singleton);

            builder.Register<CreditsModel>(Lifetime.Singleton);
            builder.Register<AudioVolumeModel>(Lifetime.Singleton);
            builder.Register<AudioVolumeService>(Lifetime.Singleton);
            builder.Register<TeamColorModel>(Lifetime.Singleton);
            builder.Register<TerritoriesModel>(Lifetime.Singleton)
                .AsSelf()
                .As<ISelectedTerritory>();

            builder.Register<ActorsUpgradeModel>(Lifetime.Singleton)
                .AsSelf()
                .As<IActorsUpgradeModel>();

            builder.RegisterComponent<IMusic>(_music);
            builder.RegisterComponent<ISfx>(_sfx);
            builder.RegisterComponent<ISceneLoader>(_sceneLoader);
            builder.RegisterComponent<IUpdater>(_updater);
            builder.RegisterInstance(_mixer);
            builder.RegisterInstance(_actorsUpgradeConfig);
            builder.RegisterInstance(_teamColorSetConfig);
            builder.RegisterInstance(_territoryConfigs as IReadOnlyList<TerritoryConfig>);
            builder.RegisterInstance<IAllActorConfigs>(_allActorConfigs);
            builder.Register<IPlayerAvailableActorProvider, PlayerAvailableActorProvider>(Lifetime.Singleton);

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<AudioVolumeService>();
                container.Resolve<PurchaseMediator>();
            });

            RegisterScreenshotSystem(builder);
        }

        private void RegisterScreenshotSystem(IContainerBuilder builder)
        {
            builder.Register<IPreviewCreator, PreviewCreator>(Lifetime.Singleton);
            builder.Register<IPreviewInstanceFactory, PreviewInstanceFactory>(Lifetime.Singleton);
            builder.Register<IScreenshoter, Screenshoter>(Lifetime.Singleton);
            builder.Register<IScreenshotCaptureCoordinator, ScreenshotCaptureCoordinator>(Lifetime.Singleton);
            builder.Register<IRendererCapturer, RendererCapturer>(Lifetime.Singleton);
            builder.Register<IRenderTextureFactory, RenderTextureFactory>(Lifetime.Singleton);
            builder.Register<IModelCenterCalculator, ModelCenterCalculator>(Lifetime.Singleton);
            builder.RegisterComponent(_captureCamera).As<ICaptureCamera>();
        }
    }
}