using BattleBase.Services.Audio;
using BattleBase.Services.InputReader;
using BattleBase.Services.Localization;
using BattleBase.Services.PauseService;
using BattleBase.Services.SaveService;
using BattleBase.Services.SceneLoadingService;
using BattleBase.Services.UpdateService;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.DI
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private AudioService _audioService;
        [SerializeField] private Updater _updater;
        [SerializeField] private SceneLoader _sceneLoader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IInputReader, InputReader>(Lifetime.Singleton);
            builder.Register<IPauseSwitcher, PauseSwitcher>(Lifetime.Singleton);
            builder.Register<ISaveSystem, YandexGameSaveSystemAdapter>(Lifetime.Singleton);
            builder.Register<ISaver, Saver>(Lifetime.Singleton);
            builder.RegisterComponent<IAudioService>(_audioService);
            builder.RegisterComponent<ISceneLoader>(_sceneLoader);
            builder.RegisterComponent<IUpdater>(_updater);
        }
    }
}