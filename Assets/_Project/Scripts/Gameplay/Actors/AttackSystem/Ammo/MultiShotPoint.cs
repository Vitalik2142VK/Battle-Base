using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class MultiShotPoint : MonoBehaviour, IShotPoint
    {
        [SerializeField] private ShotPoint[] _shotPoints;

        private int _currentIndexPoint = 0;

        public Vector3 Position => GetPosition();

        private Vector3 GetPosition()
        {
            if (_currentIndexPoint >= _shotPoints.Length)
                _currentIndexPoint = 0;

            return _shotPoints[_currentIndexPoint++].Position;
        }
    }
}