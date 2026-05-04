using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [System.Serializable]
    public class UnitData : IUnitData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description;

        public Sprite Icon => _icon;

        public ILanguageVisitor Name => _name;

        public ILanguageVisitor Description => _description;
    }
}
