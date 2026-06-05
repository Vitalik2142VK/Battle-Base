using BattleBase.Gameplay.Actors.AI.Transition;
using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AI
{
    [CreateAssetMenu(
    fileName = nameof(StateMachineSource),
    menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(StateMachineSource))]
    public class StateMachineSource : ActorComponentSource, IStateMachineSource
    {
        [SerializeField] private StateTransitionType[] _types;

        public IEnumerable<StateTransitionType> TransitionTypes => _types;
    }
}