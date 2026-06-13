using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [System.Serializable]
    public class PriorityActorType : IPriorityActorType
    {
        [SerializeField] private ActorMask _actorMask;
        [SerializeField][Range(1f, 3f)] private float _damageCoefficient = 1f;

        public ActorMask ActorMask => _actorMask;

        public float DamageCoefficient => _damageCoefficient;
    }
}
