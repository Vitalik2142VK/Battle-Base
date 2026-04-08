using BattleBase.Static;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    [CreateAssetMenu(fileName = nameof(PopUpAudioConfig), menuName = Constants.ConfigsAssetMenuName + "/" + nameof(PopUpAudioConfig))]
    public class PopUpAudioConfig : ScriptableObject
    {
        [SerializeField] private AudioClip _showing;
        [SerializeField] private AudioClip _hiding;

        public AudioClip ShowingClip => _showing;

        public AudioClip HiddingClip => _hiding;
    }
}