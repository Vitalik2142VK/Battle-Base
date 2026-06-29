using BattleBase.AdvService;
using BattleBase.AudioService;
using BattleBase.PauseService;
using BattleBase.SaveService;
using BattleBase.SceneLoadingService;
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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPauseSwitcher, PauseSwitcher>(Lifetime.Singleton);
            builder.Register<YandexGameSaveSystemAdapter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<IAdvertisingService, YandexGameAdvertisingAdapter>(Lifetime.Singleton);
            builder.Register<CreditsModel>(Lifetime.Singleton);
            builder.Register<AudioVolumeModel>(Lifetime.Singleton);
            builder.RegisterComponent<IMusic>(_music);
            builder.RegisterComponent<ISfx>(_sfx);
            builder.RegisterComponent<ISceneLoader>(_sceneLoader);
            builder.RegisterComponent<IUpdater>(_updater);
            builder.RegisterInstance(_mixer);
        }
    }
}