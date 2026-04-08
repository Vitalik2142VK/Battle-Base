using UnityEngine;

namespace BattleBase.Services.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private Music _music;
        [SerializeField] private Sfx _sfx;

        public Music Music => _music;

        public Sfx Sfx => _sfx;
    }
}