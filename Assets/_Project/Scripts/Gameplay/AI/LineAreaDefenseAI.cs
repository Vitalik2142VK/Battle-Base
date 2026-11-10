using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Types;
using UnityEngine;

namespace BattleBase.Gameplay.AI
{
    [RequireComponent(typeof(BoxArea))]
    public class LineAreaDefenseAI : MonoBehaviour, ILineAreaDefenseAI
    {
        [SerializeField] private BuildingSite _mainSite;
        [SerializeField][Range(0, 128)] private int _maxNumberActors = 64;

        private IActorPosition[] _positions;
        private BoxArea _area;
        private int _numberActors;

        public int MainSiteId => _mainSite.Id;

        private void Awake()
        {
            _area = GetComponent<BoxArea>();
        }

        public void SetPositions(IActorPosition[] positions, int numberActors)
        {
            if (numberActors > _maxNumberActors)
                _numberActors = _maxNumberActors;
            else
                _numberActors = numberActors;

            _positions = positions ?? throw new System.ArgumentNullException(nameof(positions));
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
    }
}