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
        [SerializeField] private List<WeaponConfig> _weaponConfigs;
        [SerializeField][Min(5f)] private float _searchRadius = 30f;

        public IEnumerable<IWeaponConfig> Configs => _weaponConfigs;

        public float SearchRadius => _searchRadius;

        public bool IsSingle => _weaponConfigs.Count == 1;

        private void OnValidate()
        {
            for (int i = 0; i < _weaponConfigs.Count; i++)
            {
                if (_weaponConfigs[i] == null)
                    _weaponConfigs.RemoveAt(i--);
            }

            foreach (var weaponConfig in _weaponConfigs)
            {
                if (_searchRadius < weaponConfig.MaxRange)
                    _searchRadius = weaponConfig.MaxRange;
            }
        }
    }
}