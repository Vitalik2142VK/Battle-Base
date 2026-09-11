using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Visual.Select;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Types
{
    [RequireComponent(typeof(Selectable))]
    public class BuildingSite : ActorView, IBuildingSite
    {
        [SerializeField] private GameObject[] _hidedObjects;

        private Selectable _selectable;
        private int _id;

        public event Action Showed;

        [field: SerializeField][Range(0, 10)] private int _numberLine = 1;

        [field: SerializeField] public SiteType Type { get; private set; }

        [field: SerializeField] public TeamType Team { get; private set; }

        public int NumberLine => _numberLine;

        public int Id => _id;

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
            _id = -1;
        }

        public void Init(IBuildingSiteIdCreator idCreator)
        {
            if (idCreator == null)
                throw new ArgumentNullException(nameof(idCreator));

            if (_id < 0)
                _id = idCreator.Create();
        }

        public void Select() => 
            _selectable.TrySelect();

        public void Unselect() => 
            _selectable.Unselect();

        public void EstablishInactiveState() => 
            _selectable.SetInactiveState();

        public void Show()
        {
            foreach (var gameObject in _hidedObjects)
                gameObject.SetActive(true);

            Showed?.Invoke();
        }

        public void Hide()
        {
            foreach (var gameObject in _hidedObjects)
                gameObject.SetActive(false);
        }
    }
}