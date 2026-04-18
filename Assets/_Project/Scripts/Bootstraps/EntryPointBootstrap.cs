using BattleBase.Abstract;
using BattleBase.SceneLoadingService;
using BattleBase.Utils;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class EntryPointBootstrap : BootstrapBase, IInjectable
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader;

        private void Start() =>
            _sceneLoader.Load(Constants.MenuSceneName);
    }
}