using System;
using BattleBase.Abstract;
using BattleBase.Services.SceneLoadingService;
using BattleBase.Static;
using VContainer;

namespace BattleBase.Commands
{
    public class LoadGameSceneCommand : CommandBase, IInjectable
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader) =>
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));

        public override void Execute() =>
            _sceneLoader.Load(Constants.GameSceneName);
    }
}