using BattleBase.Core;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Weapons.Missiles
{
    public class MissileFactory : MonoBehaviour, IFactory<Missile>
    {
        [SerializeField] private Missile _misslePrefab;

        public string MissileId => _misslePrefab.Id;

        public Missile Create()
        {
            return Instantiate(_misslePrefab);
        }
    }
}