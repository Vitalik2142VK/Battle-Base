using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Multiple
{
    public class MultyShotPoint : MonoBehaviour, IMultyShotPoint
    {
        [SerializeField][SerializeIterface(typeof(IShotPoint))] private GameObject[] _gameObjectShotPoits;

        private List<IShotPoint> _shotPoints;
        private int _currentIndex;

        private void Awake()
        {
            _shotPoints = new List<IShotPoint>();

            foreach (var gameObject in _gameObjectShotPoits)
            {
                IShotPoint shotPoint = gameObject.GetComponent<IShotPoint>();
                _shotPoints.Add(shotPoint);
            }

            _currentIndex = 0;
        }

        public bool TryGetNextShotPoint(out IShotPoint shotPoint)
        {
            shotPoint = null;

            if (_currentIndex >= _shotPoints.Count)
                return false;

            shotPoint = _shotPoints[_currentIndex++];

            return true;
        }
    }
}