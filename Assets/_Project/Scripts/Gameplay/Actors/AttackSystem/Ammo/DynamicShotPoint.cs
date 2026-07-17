using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class DynamicShotPoint : MonoBehaviour, IShotPoint
    {
        [SerializeField] private ShotPoint[] _shotPoints;

        private int _currentIndexPoint = 0;

        public IShotPointTransform ShotPointTransform => GetShotPoint();

        private IShotPointTransform GetShotPoint()
        {
            if (_currentIndexPoint >= _shotPoints.Length)
                _currentIndexPoint = 0;

            return _shotPoints[_currentIndexPoint++].ShotPointTransform;
        }
    }
}