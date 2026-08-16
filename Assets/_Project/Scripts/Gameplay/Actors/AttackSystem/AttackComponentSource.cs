using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    [CreateAssetMenu(
    fileName = nameof(AttackComponentSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(AttackComponentSource))]
    public class AttackComponentSource : ActorComponentSource, IAttackComponentSource
    {
        [SerializeField] private List<WeaponConfig> _weaponConfig;
        [SerializeField][Min(5f)] private float _searchRadius = 30f;

        private void OnValidate()
        {
            for (int i = 0; i < _weaponConfig.Count; i++)
            {
                if (_weaponConfig[i] == null)
                    _weaponConfig.RemoveAt(i--);
            }

            foreach (var weaponConfig in _weaponConfig)
            {
                if (_searchRadius < weaponConfig.MaxRange)
                    _searchRadius = weaponConfig.MaxRange;
            }
        }

        public IEnumerable<IWeaponConfig> Configs => _weaponConfig;

        public float SearchRadius => _searchRadius;

        public bool IsSingle => _weaponConfig.Count == 1;
    }
}