using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.HealthSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Types
{
    public class Base : ActorView, IBase
    {
        [SerializeField] private TeamType _team;

        public TeamType Team => _team;

        private void Reset()
        {
            if (gameObject.TryGetComponent(out HealthView _) == false)
                gameObject.AddComponent<HealthView>();

            if (gameObject.TryGetComponent(out ColoredActorView _) == false)
                gameObject.AddComponent<ColoredActorView>();
        }
    }
}