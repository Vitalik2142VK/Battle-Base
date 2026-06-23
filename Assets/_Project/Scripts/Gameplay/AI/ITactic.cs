namespace BattleBase.Gameplay.AI
{
    public interface ITactic
    {
        public bool CanAction();

        public ICommand GetCommand();
    }
}