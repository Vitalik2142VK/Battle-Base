namespace BattleBase.Gameplay.Actors.AI
{
    public interface IActorState
    {
        public void Enter();

        public void Exit();

        public void Update(float delta);
    }
}