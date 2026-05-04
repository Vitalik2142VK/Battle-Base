namespace BattleBase.Gameplay.Actors
{
    public class ActorBuilder
    {
        private readonly IActor _actor;

        public ActorBuilder()
        {
            _actor = new Actor();
        }

        public void AddComponent<T>(T component) where T : class, IActorComponent
        {
            _actor.Add(component);
        }

        public IActor Build()
        {
            return _actor;
        }
    }
}
