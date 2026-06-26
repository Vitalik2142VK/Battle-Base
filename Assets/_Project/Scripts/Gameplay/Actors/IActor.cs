using BattleBase.Gameplay.Actors.Production;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public interface IActor : IUpdateable, ITeamable, IColored
    {
        public IProductionData Data { get; }

        public bool IsEnabled { get; }

        public bool IsStatic { get; }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent;

        public void SetTeam(TeamType teamType);

        public void ChangeColor(Color color);
    }
}
