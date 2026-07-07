using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    [CreateAssetMenu(
    fileName = nameof(AttackComponentSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(AttackComponentSource))]
    public class AttackComponentSource : ActorComponentSource, IAttackComponentSource
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        public IWeaponConfig Config => _weaponConfig;
    }
}