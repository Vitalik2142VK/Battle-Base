using BattleBase.Core;

namespace BattleBase.Gameplay.AI
{
    public interface IBrain
    {
        public void Init();

        public bool TryGetCommand(out ICommand command);
    }
}