namespace BattleBase.Services.SceneLoadingService
{
    public interface ISceneLoader
    {
        public string CurrentSceneName { get; }

        public void Load(string name);

        public void ReloadCurrentScene();
    }
}