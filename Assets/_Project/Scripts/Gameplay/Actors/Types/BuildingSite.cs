using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Visual.Select;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Types
{
    [RequireComponent(typeof(Selectable))]
    public class BuildingSite : ActorView, IBuildingSite
    {
        private Selectable _selectable;

        [field: SerializeField] public TeamType Team { get; private set; }

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void IstablishInactiveState() => 
            _selectable.SetInactiveState();
    }
}