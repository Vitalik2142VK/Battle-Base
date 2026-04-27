using System;
using BattleBase.DI;
using BattleBase.SceneLoadingService;
using BattleBase.Utils;
using VContainer;

namespace BattleBase.Commands
{
    public class LoadMenuSceneCommand : CommandBase, IInjectable
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));

        public override void Execute() =>
            _sceneLoader.Load(Constants.MenuSceneName);
    }
}