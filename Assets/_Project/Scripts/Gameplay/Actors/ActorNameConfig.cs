using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
        fileName = nameof(ActorNameConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorNameConfig))]
    public class ActorNameConfig : ScriptableObject
    {
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private LanguageTextsSet _description;

        public ILanguageTextsSet Name => _unitName;

        public ILanguageTextsSet Description => _description;
    }
}