using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [System.Serializable]
    public class ProjectileConfig : IProjectileConfig
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField][Min(10f)] private float _speed = 50f;

        public string ProjectileId => _projectilePrefab.name;

        public float Speed => _speed;
    }
}