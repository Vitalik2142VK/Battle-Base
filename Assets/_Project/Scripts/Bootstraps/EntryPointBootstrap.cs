using BattleBase.Abstract;
using BattleBase.Services.SceneLoadingService;
using BattleBase.Static;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class EntryPointBootstrap : BootstrapBase
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        private void Start() =>
            _sceneLoader.Load(Constants.MenuSceneName);
    }
}