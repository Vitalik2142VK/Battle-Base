using BattleBase.Gameplay.Actors.Spawn;
using UnityEngine;

namespace BattleBase.Gameplay.Actors 
{
    public interface IActorView : IActorPosition
    {
        public bool TryGetViewComponent<T>(out T component) where T : class, IActorViewComponent;

        public void SetActive(bool isActive);

        public void SetSpawnData(ISpawnPoint spawnData);
    }
}
