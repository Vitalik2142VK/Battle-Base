using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Weapons;
using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
        fileName = nameof(ActorConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(ActorConfig))]
    public class ActorConfig : ScriptableObject
    {
        [SerializeField] private ActorView _prefab;
        [SerializeField] private UnitData _data;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField][Min(3f)] private float _constructionTime = 5f;

        [SerializeField] private List<ActorComponentSource> _components;

        public ActorView Prefab => _prefab;

        public IUnitData Info => _data;

        public IWeaponConfig WeaponConfig => _weaponConfig;

        public IMovementConfig MovementConfig => _movementConfig;

        public float ConstructionTime => _constructionTime;

        public IEnumerable<IComponentSource> GetComponentSources()
        {
            return _components;
        }
    }
}
