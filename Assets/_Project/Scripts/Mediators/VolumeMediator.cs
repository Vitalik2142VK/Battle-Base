using BattleBase.AudioService;
using BattleBase.Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BattleBase.Mediators
{
    public class VolumeMediator : MediatorBase
    {
        

        [SerializeField] private Slider _generalSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        

        public override void Init()
        {
            
        }

        
    }
}