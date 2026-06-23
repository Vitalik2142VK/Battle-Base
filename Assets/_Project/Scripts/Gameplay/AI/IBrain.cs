namespace BattleBase.Gameplay.AI
{
    public interface IBrain
    {
        public bool TryGetCommand(out ICommand command);
    }
}