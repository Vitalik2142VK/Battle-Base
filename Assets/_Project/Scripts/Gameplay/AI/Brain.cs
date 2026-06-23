namespace BattleBase.Gameplay.AI
{
    public class Brain : IBrain
    {
        private readonly ITactic _mainTactic;

        public Brain(ITactic mainTactic)
        {
            _mainTactic = mainTactic ?? throw new System.ArgumentNullException(nameof(mainTactic));
        }

        public bool TryGetCommand(out ICommand command)
        {
            command = null;

            if (_mainTactic.CanAction())
            {
                command = _mainTactic.GetCommand();

                return true;
            }

            return false;
        }
    }
}