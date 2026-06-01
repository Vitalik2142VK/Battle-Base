using BattleBase.Gameplay.Actors.Spawn;

namespace BattleBase.Gameplay.Actors 
{
    public interface IActorView
    {
        public bool TryGetViewComponent<T>(out T component) where T : class, IActorViewComponent;

        public void SetActive(bool isActive);

        public void SetSpawnData(ISpawnData spawnData);
    }
}
