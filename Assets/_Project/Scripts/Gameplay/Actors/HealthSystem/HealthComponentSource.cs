using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
        fileName = nameof(HealthComponentSource),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(HealthComponentSource))]
    public class HealthComponentSource : ActorComponentSource, IComponentSource
    {
        [SerializeField] private HealthConfig _healthConfig;

        public override IActorComponent Create()
        {
            return new Health(_healthConfig, new DamageModifier());
        }
    }
}
