using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    [CreateAssetMenu(
    fileName = nameof(AttackComponentSource),
    menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(AttackComponentSource))]
    public class AttackComponentSource : ActorComponentSource, IAttackComponentSource
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        public IWeaponConfig Config => _weaponConfig;
    }
}