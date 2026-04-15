using UnityEngine;

namespace BattleBase.AudioService
{
    public class Audio : MonoBehaviour, IAudioService
    {
        [SerializeField] private Music _music;
        [SerializeField] private Sfx _sfx;

        public IMusic Music => _music;

        public ISfx Sfx => _sfx;
    }
}