using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface ISpawnData
    {
        public Vector3 SpawnPosition { get; }

        public Quaternion SpawnRotation { get; }
    }
}
