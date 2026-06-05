using System;

namespace BattleBase.Gameplay.Actors.AI
{
    public class ActorStateMachineFactory : IComponentFactory
    {
        public Type SourceType => typeof(StateMachineSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IStateMachineSource stateMachineSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IStateMachineSource)}");

            return new ActorStateMachine(stateMachineSource.TransitionTypes);
        }
    }
}