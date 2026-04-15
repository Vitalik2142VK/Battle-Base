using BattleBase.AdvService;
using BattleBase.AudioService;
using BattleBase.PauseService;
using BattleBase.SaveService;
using BattleBase.SceneLoadingService;
using BattleBase.UpdateService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private Audio _audioService;
        [SerializeField] private Updater _updater;
        [SerializeField] private SceneLoader _sceneLoader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IPauseSwitcher, PauseSwitcher>(Lifetime.Singleton);
            builder.Register<ISaveSystem, YandexGameSaveSystemAdapter>(Lifetime.Singleton);
            builder.Register<IAdvService, YandexGameAdvAdapter>(Lifetime.Singleton);
            builder.Register<Saver>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponent<IAudioService>(_audioService);
            builder.RegisterComponent<ISceneLoader>(_sceneLoader);
            builder.RegisterComponent<IUpdater>(_updater);
        }
    }
}