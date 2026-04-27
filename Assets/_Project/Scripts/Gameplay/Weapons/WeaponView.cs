using UnityEngine;

namespace BattleBase.Gameplay.Weapons
{
    public class WeaponView : MonoBehaviour, IWeaponView
    {
        [SerializeField] private ParticleSystem _particleShot;

        public void PlayShot()
        {
            _particleShot.Play();
        }
    }
}