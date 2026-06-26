using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class SpawnPoint : MonoBehaviour, ISpawnPoint
    {
        private Transform _transform;

        public Vector3 SpawnPosition => _transform.position;

        public Quaternion SpawnRotation => _transform.rotation;

        private void Awake()
        {
            _transform = transform;
        }
    }
}
