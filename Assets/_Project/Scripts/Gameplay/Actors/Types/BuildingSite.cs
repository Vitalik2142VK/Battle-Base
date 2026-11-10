using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Visual.Select;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Types
{
    [RequireComponent(typeof(Selectable))]
    public class BuildingSite : ActorView, IBuildingSite
    {
        [SerializeField] private BuildingSite[] _nearbySites;
        [SerializeField] private GameObject[] _hidedObjects;

        private List<int> _nearbySiteIds;
        private Selectable _selectable;
        private int _id;

        public event Action Showed;

        [field: SerializeField][Range(0, 10)] private int _numberLine = 1;

        [field: SerializeField] public SiteType Type { get; private set; }

        [field: SerializeField] public TeamType Team { get; private set; }

        public IEnumerable<int> NearbySiteIds => _nearbySiteIds;

        public int NumberLine => _numberLine;

        public int Id => _id;

        [Inject]
        public void Construct(IBuildingSiteIdCreator idCreator)
        {
            if (idCreator == null)
                throw new ArgumentNullException(nameof(idCreator));

            _id = idCreator.Create();
        }

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
            _nearbySiteIds = new List<int>();

            foreach (var site in _nearbySites)
                _nearbySiteIds.Add(site.Id);
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