using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using System;
using System.Collections;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.AI
{
    [RequireComponent(typeof(BoxArea))]
    public class AreaDefenseAI : MonoBehaviour, IAreaDefenseAI
    {
        [SerializeField][Range(0.25f, 3f)] private float _checkTime = 1f;
        [SerializeField][Range(32, 256)] private int _maxFoundNumberActors = 128;
        [SerializeField] private TeamType _team = TeamType.Enemy;

        private IActorPosition[] _positions;
        private IActorsStorage _actorsStorage;
        private BoxArea _area;
        private WaitForSeconds _wait;
        private int _numberActors;

        private void Awake()
        {
            _area = GetComponent<BoxArea>();
            _positions = new IActorPosition[_maxFoundNumberActors];
            _wait = new WaitForSeconds(_checkTime);
            _numberActors = 0;
        }

        private void Start()
        {
            StartCoroutine(CheckArea());
        }

        [Inject]
        public void Construct(IActorsStorage actorsStorage)
        {
            _actorsStorage = actorsStorage ?? throw new ArgumentNullException(nameof(actorsStorage));
        }

        public int GetNumberActorsInArea()
        {
            int result = 0;
            IActorPosition current;

            for (int i = 0; i < _numberActors; i++)
            {
                current = _positions[i];

                if (_area.HasInArea(current.Position))
                    result++;
            }

            return result;
        }

        private IEnumerator CheckArea()
        {
            while (gameObject.activeSelf)
            {
                yield return _wait;

                _numberActors = _actorsStorage.GetActorPositionsOtherTeam(_positions, _team);
            }
        }
    }
}