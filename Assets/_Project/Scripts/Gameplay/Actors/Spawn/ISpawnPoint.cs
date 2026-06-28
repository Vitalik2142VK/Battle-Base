using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface ISpawnPoint : IActorViewComponent
    {
        public Vector3 SpawnPosition { get; }

        public Quaternion SpawnRotation { get; }
    }
}
