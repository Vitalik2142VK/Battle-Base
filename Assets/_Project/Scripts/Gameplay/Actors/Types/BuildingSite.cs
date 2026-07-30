using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Visual.Select;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Types
{
    [RequireComponent(typeof(Selectable))]
    public class BuildingSite : ActorView, IBuildingSite
    {
        private Selectable _selectable;

        [field: SerializeField][Range(0, 10)] private int _numberLine = 1;

        [field: SerializeField] public TeamType Team { get; private set; }

        public int NumberLine => _numberLine;

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        public void EstablishInactiveState() => 
            _selectable.SetInactiveState();
    }
}