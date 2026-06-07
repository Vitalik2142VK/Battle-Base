using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public interface IActor : IUpdateable, ITeamable, IColored
    {
        public IActorData Data { get; }

        public bool IsEnabled { get; }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent;

        public void SetTeam(TeamType teamType);

        public void ChangeColor(Color color);
    }
}
