using BattleBase.Core;

namespace BattleBase.Gameplay.AI
{
    public interface IBrain
    {
        public bool ThinkCompleted { get; }

        public void Init();

        public void ThinkDuringTick();

        public bool TryGetCommand(out ICommand command);
    }
}