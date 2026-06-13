using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    [CreateAssetMenu(
        fileName = nameof(HealthComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(HealthComponentSource))]
    public class HealthComponentSource : ActorComponentSource, IHealthComponentSource
    {
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField][SingleFlag] private ActorMask _type = ActorMask.Building;

        public IHealthConfig Config => _healthConfig;

        public ActorMask Type => _type;
    }
}
