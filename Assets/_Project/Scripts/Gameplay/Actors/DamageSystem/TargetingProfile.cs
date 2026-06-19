using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [CreateAssetMenu(
        fileName = nameof(TargetingProfile),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(TargetingProfile))]
    public class TargetingProfile : ScriptableObject, ITargetingProfile
    {
        [SerializeField] private ActorMask _notAttacked;
        [SerializeField] private PriorityActorType[] _priorityActorTypes;

        public ActorMask NotAttacked => _notAttacked;

        public IEnumerable<IPriorityActorType> PriorityActorTypes => _priorityActorTypes;
    }
}
